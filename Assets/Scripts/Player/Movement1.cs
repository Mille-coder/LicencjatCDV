using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public int movespeed = 1;
    [SerializeField] public int jumppower;
    [SerializeField] bool onLedge = false;
    [SerializeField] GameObject InteractionRange;
    [SerializeField] PlayerSounds PlayerSounds;

    [Header("Animation Locks")]
    [SerializeField] private float pickUpLockTime = 0.8f;
    [SerializeField] private float axeSwingLockTime = 0.6f;
    [SerializeField] private float loseBalanceLockTime = 1.0f;

    private Ledge activeLedge;
    private bool pushing = false;

    private bool grounded = true;
    private bool hanging = false;

    private Rigidbody playerRB;
    private Animator animator;

    private bool isPickingUp = false;
    private bool isSwingingAxe = false;
    private bool isLosingBalance = false;

    int slow = 1;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        GlobalEvents.OnSlowOff += SlowOff;
        GlobalEvents.OnSlowOn += SlowOn;
    }

    private void OnDisable()
    {
        GlobalEvents.OnSlowOff -= SlowOff;
        GlobalEvents.OnSlowOn -= SlowOn;
    }

    void Update()
    {
        if (isPickingUp || isSwingingAxe || isLosingBalance)
        {
            playerRB.velocity = new Vector3(0f, playerRB.velocity.y, 0f);
            UpdateAnimations();
            return;
        }

        if (onLedge == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = activeLedge.Gettargetpos();
                onLedge = false;
                playerRB.isKinematic = false;
            }
        }

        if (onLedge == false)
        {
            if (Input.GetButtonDown("Jump") && grounded)
            {
                grounded = false;

                playerRB.velocity = new Vector3(
                    playerRB.velocity.x,
                    jumppower,
                    0f
                );

                animator.SetTrigger("Jump");
            }

            if (grounded == true)
            {
                playerRB.velocity = new Vector3(
                    Input.GetAxisRaw("Horizontal") * movespeed / slow,
                    playerRB.velocity.y,
                    0f
                );
            }

            if (Input.GetButtonUp("Jump"))
            {
                playerRB.velocity = new Vector3(
                    playerRB.velocity.x,
                    playerRB.velocity.y * 0.5f,
                    0f
                );
            }

            if (Input.GetButtonUp("Horizontal"))
            {
                playerRB.velocity = new Vector3(
                    playerRB.velocity.x * 0.5f,
                    playerRB.velocity.y,
                    0f
                );
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartPickUp();
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartAxeSwing();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            LoseBalance();
        }

        UpdateAnimations();
    }

    void FixedUpdate()
    {
        if (playerRB.velocity.x < 0)
            Turn(true);

        if (playerRB.velocity.x > 0)
            Turn(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = false;
        }
    }

    void Turn(bool direction)
    {
        if (pushing == false)
        {
            if (direction == true)
                transform.rotation = Quaternion.Euler(0, -90, 0);
            else
                transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void Grabledge(Ledge currentLedge)
    {
        onLedge = true;
        activeLedge = currentLedge;
        playerRB.isKinematic = true;
    }

    public void Push()
    {
        pushing = !pushing;
    }

    private void SlowOn()
    {
        slow = 2;
    }

    private void SlowOff()
    {
        slow = 1;
    }

    private void UpdateAnimations()
    {
        if (animator == null) return;

        float xSpeed = Mathf.Abs(playerRB.velocity.x);

        animator.SetBool("isRunning", xSpeed > 0.1f && grounded);

        animator.SetBool("IsGrounded", grounded);
        animator.SetBool("IsPickingUp", isPickingUp);
        animator.SetBool("IsSwingingAxe", isSwingingAxe);
        animator.SetBool("IsLosingBalance", isLosingBalance);
    }

    private void StartPickUp()
    {
        if (isPickingUp || isSwingingAxe || isLosingBalance) return;

        animator.SetTrigger("PickUp");
        StartCoroutine(PickUpLock());
    }

    private IEnumerator PickUpLock()
    {
        isPickingUp = true;
        playerRB.velocity = new Vector3(0f, playerRB.velocity.y, 0f);

        yield return new WaitForSeconds(pickUpLockTime);

        isPickingUp = false;
    }

    private void StartAxeSwing()
    {
        if (isPickingUp || isSwingingAxe || isLosingBalance) return;

        animator.SetTrigger("AxeSwing");
        StartCoroutine(AxeSwingLock());
    }

    private IEnumerator AxeSwingLock()
    {
        isSwingingAxe = true;
        playerRB.velocity = new Vector3(0f, playerRB.velocity.y, 0f);

        yield return new WaitForSeconds(axeSwingLockTime);

        isSwingingAxe = false;
    }

    public void LoseBalance()
    {
        if (isPickingUp || isSwingingAxe || isLosingBalance) return;

        animator.SetTrigger("LoseBalance");
        StartCoroutine(LoseBalanceLock());
    }

    private IEnumerator LoseBalanceLock()
    {
        isLosingBalance = true;
        playerRB.velocity = new Vector3(0f, playerRB.velocity.y, 0f);

        yield return new WaitForSeconds(loseBalanceLockTime);

        isLosingBalance = false;
    }

    private void PlayFootsteps()
    {
        PlayerSounds.PlayFootsteps();
    }
}