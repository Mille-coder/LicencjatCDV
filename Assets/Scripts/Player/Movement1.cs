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
    private Rigidbody playerRB;

    int slow = 1;

    [SerializeField] private Animator animator;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        if (animator == null)
            Debug.LogError("No Animator found on Player or its children!");
        else
            Debug.Log("Animator found on: " + animator.gameObject.name);
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
                playerRB.velocity = new Vector3(
                    Input.GetAxisRaw("Horizontal") * movespeed / slow,
                    playerRB.velocity.y,
                    playerRB.velocity.z
                );

                if (Input.GetButtonDown("Jump") && pushing == false)
                {
                    playerRB.velocity = new Vector3(
                        playerRB.velocity.x,
                        jumppower,
                        playerRB.velocity.z
                    );

                    grounded = false;

                    if (animator != null)
                        animator.SetTrigger("Jump");
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                playerRB.velocity = new Vector3(
                    playerRB.velocity.x,
                    playerRB.velocity.y * 0.5f,
                    playerRB.velocity.z
                );
            }

            if (Input.GetButtonUp("Horizontal"))
            {
                playerRB.velocity = new Vector3(
                    playerRB.velocity.x * 0.5f,
                    playerRB.velocity.y,
                    playerRB.velocity.z
                );
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (animator != null)
            {
                animator.SetBool("isRunning", true);
                animator.Play("rig|Run", 0, 0f);
                Debug.Log("Forced rig|Run animation and set isRunning true");
            }
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
        if (animator == null)
        {
            Debug.LogError("Animator is missing!");
            return;
        }

        bool isRunning = Mathf.Abs(playerRB.velocity.x) > 0.1f && grounded && !onLedge;

        animator.SetBool("isRunning", isRunning);

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        Debug.Log(
            "isRunning: " + animator.GetBool("isRunning") +
            " | In transition: " + animator.IsInTransition(0) +
            " | Idle: " + state.IsName("rig|Idle") +
            " | Run: " + state.IsName("rig|Run") +
            " | Normalized time: " + state.normalizedTime
        );
    }

    private void PlayFootsteps()
    {
        PlayerSounds.PlayFootsteps();
    }
}