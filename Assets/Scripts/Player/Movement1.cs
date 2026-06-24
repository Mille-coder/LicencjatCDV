using System.Collections;
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
    
    [Header("Ledge Animation")]
    [SerializeField] private string isOnLedgeBool = "isOnLedge";
    [SerializeField] private string climbTrigger = "Climb";
    [SerializeField] private float ledgeClimbFinishDelay = 0.8f;
    [SerializeField] private bool finishClimbWithAnimationEvent = true;

    [Header("Ledge Grab Protection")]
    [SerializeField] private float ledgeGrabCooldownAfterClimb = 0.5f;

    private bool isClimbingLedge = false;
    private Coroutine ledgeClimbRoutine;
    private float nextAllowedLedgeGrabTime = 0f;

    public bool IsOnLedge => onLedge || isClimbingLedge;
    
    
    
    [SerializeField] private CarryPairAnimator carryPairAnimator;
    [SerializeField] private Equipment equipment;
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

        if (carryPairAnimator == null)
        {
            carryPairAnimator = GetComponent<CarryPairAnimator>();
        }

        if (equipment == null)
        {
            equipment = GetComponent<Equipment>();
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
        if (!onLedge || isClimbingLedge)
            return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            StartLedgeClimb();
        }
    }
    private void StartLedgeClimb()
    {
        if (activeLedge == null)
            return;

        isClimbingLedge = true;

        if (playerRB != null)
        {
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
        }

        if (firefighterAnimator != null)
        {
            firefighterAnimator.SetBool(isOnLedgeBool, false);
            firefighterAnimator.SetBool("isRunning", false);
            firefighterAnimator.SetBool("isHoldingTrash", false);
            firefighterAnimator.ResetTrigger(climbTrigger);
            firefighterAnimator.SetTrigger(climbTrigger);
        }

        if (ledgeClimbRoutine != null)
            StopCoroutine(ledgeClimbRoutine);

        ledgeClimbRoutine = StartCoroutine(FinishLedgeClimbAfterDelay());
    }
    
    private IEnumerator FinishLedgeClimbAfterDelay()
    {
        yield return new WaitForSeconds(ledgeClimbFinishDelay);

        if (onLedge || isClimbingLedge)
        {
            FinishLedgeClimb();
        }
    }
    
    public void FinishLedgeClimb()
    {
        if (!onLedge && !isClimbingLedge)
            return;

        nextAllowedLedgeGrabTime = Time.time + ledgeGrabCooldownAfterClimb;

        Vector3 targetPosition = transform.position;

        if (activeLedge != null)
        {
            targetPosition = activeLedge.Gettargetpos();
        }

        onLedge = false;
        isClimbingLedge = false;
        activeLedge = null;
        grounded = true;

        if (playerRB != null)
        {
            playerRB.isKinematic = false;
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
            playerRB.position = targetPosition;
        }
        else
        {
            transform.position = targetPosition;
        }

        if (firefighterAnimator != null)
        {
            firefighterAnimator.SetBool(isOnLedgeBool, false);
            firefighterAnimator.SetBool("isRunning", false);
        }

        ledgeClimbRoutine = null;
    }
    private void HandleMovementInput()
    {
        if (playerRB == null)
            return;

        if (onLedge || isClimbingLedge)
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

                bool carryingWoman = equipment != null && equipment.haswoman;

                if (carryingWoman)
                {
                    if (carryPairAnimator != null)
                        carryPairAnimator.PlayJumpWithWoman();
                }
                else
                {
                    if (firefighterAnimator != null)
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

        bool isBusyOnLedge = onLedge || isClimbingLedge;

        bool isRunning = Mathf.Abs(playerRB.velocity.x) > 0.1f
                         && grounded
                         && !isBusyOnLedge;

        if (isBusyOnLedge)
        {
            isRunning = false;
        }

        firefighterAnimator.SetBool("isRunning", isRunning);
        firefighterAnimator.SetBool("isHoldingTrash", isHoldingTrash);

        if (equipment != null && equipment.haswoman && carryPairAnimator != null && !isBusyOnLedge)
        {
            carryPairAnimator.SetCarryWalking(isRunning);
        }

        if (onLedge && !isClimbingLedge)
        {
            firefighterAnimator.SetBool(isOnLedgeBool, true);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = true;
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
        if (currentLedge == null)
            return;

        if (Time.time < nextAllowedLedgeGrabTime)
            return;

        if (onLedge || isClimbingLedge)
            return;

        if (pushing || isHoldingTrash)
            return;

        onLedge = true;
        isClimbingLedge = false;
        activeLedge = currentLedge;
        grounded = false;

        if (playerRB != null)
        {
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
            playerRB.isKinematic = true;
            playerRB.position = currentLedge.GetHangPosition();
        }
        else
        {
            transform.position = currentLedge.GetHangPosition();
        }

        transform.rotation = currentLedge.GetHangRotation();

        if (firefighterAnimator != null)
        {
            firefighterAnimator.SetBool("isRunning", false);
            firefighterAnimator.SetBool("isHoldingTrash", false);
            firefighterAnimator.SetBool(isOnLedgeBool, true);
        }
    }

    public void Push()
    {
        if (onLedge || isClimbingLedge)
            return;

        pushing = !pushing;
        isHoldingTrash = pushing;

        if (firefighterAnimator != null)
        {
            firefighterAnimator.SetBool("isHoldingTrash", isHoldingTrash);

            if (!isHoldingTrash)
            {
                firefighterAnimator.SetBool("isRunning", false);
            }
        }
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