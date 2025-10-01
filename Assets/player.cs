using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Animator playerAnim;
    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    public bool walking;
    public Transform playerTrans;

    // Character Controller variables
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    // Gravity and Jump - INCREASED VALUES FOR MORE DISTANCE
    public float gravity = -15f; // Reduced gravity for longer air time
    public float jumpHeight = 6f; // Doubled jump height
    public float groundCheckDistance = 0.3f;
    public LayerMask groundLayer;

    // Jump delay variables
    public float jumpDelay = 0.2f;
    private float lastJumpPressTime = -999f;
    private bool jumpInputQueued = false;

    // Track current movement state
    private string currentState = "idle";
    private bool isRunning = false;

    // Air control and momentum
    public float airControl = 0.8f; // How much control you have in air
    public float jumpMomentumMultiplier = 1.2f; // Extra speed during jump

    void Start()
    {
        controller = GetComponent<CharacterController>();
        olw_speed = w_speed;
    }

    void Update()
    {
        HandleJumpInput();
        HandleMovement();
        HandleAnimations();
    }

    void HandleJumpInput()
    {
        // Record when jump button is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            lastJumpPressTime = Time.time;
            jumpInputQueued = true;

            // Store current state before jumping
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                currentState = "run";
                isRunning = true;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                currentState = "walk";
                isRunning = false;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                currentState = "walkback";
                isRunning = false;
            }
            else
            {
                currentState = "idle";
                isRunning = false;
            }

            // Play jump animation immediately
            playerAnim.ResetTrigger("idle");
            playerAnim.ResetTrigger("walk");
            playerAnim.ResetTrigger("walkback");
            playerAnim.ResetTrigger("run");
            playerAnim.SetTrigger("jump");
        }

        // If jump was pressed recently and we're now grounded, execute the jump
        if (jumpInputQueued && isGrounded && (Time.time - lastJumpPressTime) <= jumpDelay)
        {
            ExecuteJump();
            jumpInputQueued = false;
        }

        // Clear queued jump if too much time has passed
        if (jumpInputQueued && (Time.time - lastJumpPressTime) > jumpDelay)
        {
            jumpInputQueued = false;
        }
    }

    void ExecuteJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        Debug.Log("Jump executed with delay!");
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

            // Restore previous state when landing
            if (!jumpInputQueued && (Time.time - lastJumpPressTime) > jumpDelay)
            {
                RestoreMovementState();
            }
        }

        // Get movement input
        Vector3 movement = Vector3.zero;
        float currentSpeed = 0f;

        // Forward movement
        if (Input.GetKey(KeyCode.W))
        {
            movement = transform.forward;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = w_speed;
                isRunning = true;
                currentState = "run";
            }
            else
            {
                currentSpeed = w_speed;
                isRunning = false;
                currentState = "walk";
            }
            walking = true;
        }
        // Backward movement
        else if (Input.GetKey(KeyCode.S))
        {
            movement = -transform.forward;
            currentSpeed = wb_speed;
            isRunning = false;
            currentState = "walkback";
            walking = true;
        }
        else
        {
            walking = false;
            isRunning = false;
            currentState = "idle";
        }

        // Apply movement with enhanced air control and momentum
        if (controller != null && movement != Vector3.zero)
        {
            float controlMultiplier = isGrounded ? 1f : airControl;

            // Apply momentum boost during jump
            if (!isGrounded && Input.GetKey(KeyCode.W))
            {
                controlMultiplier *= jumpMomentumMultiplier;
            }

            controller.Move(movement * currentSpeed * controlMultiplier * Time.deltaTime);
        }

        // Rotation
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleAnimations()
    {
        // Only update animations if not currently in jump state and grounded
        if (isGrounded && (Time.time - lastJumpPressTime) > jumpDelay)
        {
            // Update running speed based on shift key
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    w_speed = olw_speed + rn_speed;
                }
                else
                {
                    w_speed = olw_speed;
                }
            }

            // Set the appropriate animation based on current state
            playerAnim.ResetTrigger("idle");
            playerAnim.ResetTrigger("walk");
            playerAnim.ResetTrigger("walkback");
            playerAnim.ResetTrigger("run");

            switch (currentState)
            {
                case "run":
                    playerAnim.SetTrigger("run");
                    break;
                case "walk":
                    playerAnim.SetTrigger("walk");
                    break;
                case "walkback":
                    playerAnim.SetTrigger("walkback");
                    break;
                default:
                    playerAnim.SetTrigger("idle");
                    break;
            }
        }
    }

    void RestoreMovementState()
    {
        // Force update the animation state based on current input
        playerAnim.ResetTrigger("idle");
        playerAnim.ResetTrigger("walk");
        playerAnim.ResetTrigger("walkback");
        playerAnim.ResetTrigger("run");

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            playerAnim.SetTrigger("run");
            currentState = "run";
            isRunning = true;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            currentState = "walk";
            isRunning = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerAnim.SetTrigger("walkback");
            currentState = "walkback";
            isRunning = false;
        }
        else
        {
            playerAnim.SetTrigger("idle");
            currentState = "idle";
            isRunning = false;
        }
    }

    // Visualize ground check in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckDistance);
    }
}