using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public int movespeed = 1;
    [SerializeField] public int jumppower;
    [SerializeField] bool onLedge = false;
    [SerializeField] GameObject InteractionRange;
    [SerializeField] PlayerSounds PlayerSounds;

    

    private Ledge activeLedge;
    private bool pushing = false;

    private bool grounded = true;
    private bool hanging = false;
    private Rigidbody playerRB;
    
    int slow = 1;

    private Animator animator;
    [SerializeField] private float pickUpLockTime = 0.8f;
    private bool isPickingUp = false;

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
        if (isPickingUp)
        {
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
            if (grounded == true)
            {
                playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movespeed / slow, playerRB.velocity.y);

                if (Input.GetButtonDown("Jump") && pushing == false)
                {
                    playerRB.velocity = new Vector2(playerRB.velocity.x, jumppower);
                    grounded = false;
                    if (animator != null)
                        animator.SetTrigger("Jump");
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);
            }

            if (Input.GetButtonUp("Horizontal"))
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x * 0.5f, playerRB.velocity.y);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (animator != null)
                animator.SetTrigger("PickUp");
            StartCoroutine(PickUpLock());
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

        float speedParam = (xSpeed > 0.1f && grounded) ? 1f : 0f;

        animator.SetFloat("Speed", speedParam);
        animator.SetBool("IsGrounded", grounded);
    }

    private IEnumerator PickUpLock()
    {
        isPickingUp = true;
        playerRB.velocity = new Vector2(0f, playerRB.velocity.y);

        yield return new WaitForSeconds(pickUpLockTime);

        isPickingUp = false;
    }

    private void PlayFootsteps()
    {
        PlayerSounds.PlayFootsteps();
    }


}