using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public int movespeed = 1;
    [SerializeField] public int jumppower;

    [Header("State")]
    [SerializeField] private bool onLedge = false;
    [SerializeField] private bool pushing = false;
    [SerializeField] private bool isHoldingTrash = false;

    [Header("References")]
    [SerializeField] private GameObject InteractionRange;
    [SerializeField] private PlayerSounds PlayerSounds;

    [SerializeField] private Animator firefighterAnimator;
    [SerializeField] private GameObject womanModel;

    private Ledge activeLedge;
    private bool grounded = true;
    private Rigidbody playerRB;

    private int slow = 1;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        if (firefighterAnimator == null)
        {
            firefighterAnimator = GetComponentInChildren<Animator>();
        }
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

    private void Update()
    {
        HandleLedgeInput();
        HandleMovementInput();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        if (playerRB == null)
            return;

        if (playerRB.velocity.x < -0.1f)
        {
            Turn(true);
        }
        else if (playerRB.velocity.x > 0.1f)
        {
            Turn(false);
        }
    }

    private void HandleLedgeInput()
    {
        if (!onLedge)
            return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = activeLedge.Gettargetpos();
            onLedge = false;

            if (playerRB != null)
                playerRB.isKinematic = false;
        }
    }

    private void HandleMovementInput()
    {
        if (playerRB == null)
            return;

        if (onLedge)
            return;

        if (grounded)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            playerRB.velocity = new Vector3(
                horizontalInput * movespeed / slow,
                playerRB.velocity.y,
                playerRB.velocity.z
            );

            if (Input.GetButtonDown("Jump") && !pushing)
            {
                playerRB.velocity = new Vector3(
                    playerRB.velocity.x,
                    jumppower,
                    playerRB.velocity.z
                );

                grounded = false;

                if (firefighterAnimator != null)
                {
                    firefighterAnimator.SetTrigger("Jump");
                }
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

    private void HandleDebugAnimationInput()
    {
        if (firefighterAnimator == null)
            return;
    }
    
    private void UpdateAnimations()
    {
        if (firefighterAnimator == null || playerRB == null)
            return;

        bool isRunning = Mathf.Abs(playerRB.velocity.x) > 0.1f && grounded && !onLedge;
        bool isFalling = playerRB.velocity.y < -0.1f && !grounded;

        firefighterAnimator.SetBool("isRunning", isRunning);
        firefighterAnimator.SetBool("isGrounded", grounded);
        firefighterAnimator.SetBool("isFalling", isFalling);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = true;

            if (firefighterAnimator != null)
            {
                firefighterAnimator.SetBool("isGrounded", true);
                firefighterAnimator.SetBool("isFalling", false);
                firefighterAnimator.SetTrigger("Landing");
            }
        }
    }

    private void Turn(bool direction)
    {
        if (pushing)
            return;

        if (direction)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void Grabledge(Ledge currentLedge)
    {
        onLedge = true;
        activeLedge = currentLedge;

        if (playerRB != null)
            playerRB.isKinematic = true;
    }

    public void Push()
    {
        pushing = !pushing;
    }
   
    public void PlayDeath()
    {
        if (firefighterAnimator != null)
            firefighterAnimator.SetTrigger("Death");
    }

    private void SlowOn()
    {
        slow = 2;
    }

    private void SlowOff()
    {
        slow = 1;
    }

    private void PlayFootsteps()
    {
        if (PlayerSounds != null)
            PlayerSounds.PlayFootsteps();
    }
}