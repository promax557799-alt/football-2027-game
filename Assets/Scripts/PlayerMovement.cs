using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float sprintSpeed = 15f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float airDrag = 2f;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float currentSpeed;
    private bool canJump = true;
    private InputManager inputManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = 80f;
            rb.drag = 5f;
        }
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        inputManager = InputManager.instance;
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        
        // Handle input
        HandleMovement();
        HandleJump();
        HandleSprint();
        
        // Apply drag
        rb.drag = isGrounded ? groundDrag : airDrag;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleMovement()
    {
        Vector3 input = inputManager.GetMovementInput();
        moveDirection = transform.forward * input.z + transform.right * input.x;
        
        if (inputManager.GetSprintInput())
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
    }

    private void MovePlayer()
    {
        if (moveDirection.magnitude > 0)
        {
            rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void HandleJump()
    {
        if (inputManager.GetJumpInput() && isGrounded && canJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        canJump = false;
        
        Invoke(nameof(ResetJump), 0.5f);
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void HandleSprint()
    {
        // Sprint logic handled in HandleMovement
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
