using UnityEngine;

public class PlayerController : MonoBehaviour
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

    [Header("Ball Interaction")]
    [SerializeField] private float passForce = 20f;
    [SerializeField] private float shootForce = 30f;
    [SerializeField] private float superShotForce = 50f;
    [SerializeField] private float ballInteractionRadius = 2f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float currentSpeed;
    private bool canJump = true;
    private Ball ballScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        
        // Handle input
        HandleMovement();
        HandleJump();
        HandleBallInteraction();
        HandlePause();
        
        // Apply drag
        rb.drag = isGrounded ? groundDrag : airDrag;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = transform.forward * vertical + transform.right * horizontal;
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
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

    private void HandleBallInteraction()
    {
        // Find ball
        if (ballScript == null)
        {
            Ball ball = FindObjectOfType<Ball>();
            if (ball != null)
                ballScript = ball;
        }

        if (ballScript == null) return;

        float distanceToBall = Vector3.Distance(transform.position, ballScript.transform.position);
        
        if (distanceToBall < ballInteractionRadius)
        {
            if (Input.GetMouseButtonDown(0)) // Pass
            {
                Pass();
            }
            if (Input.GetMouseButtonDown(1)) // Shoot
            {
                Shoot();
            }
            if (Input.GetKeyDown(KeyCode.Q)) // Super Shot
            {
                SuperShot();
            }
            if (Input.GetKeyDown(KeyCode.E)) // Tackle
            {
                Tackle();
            }
        }
    }

    private void Pass()
    {
        Vector3 direction = (transform.forward).normalized;
        ballScript.ApplyForce(direction * passForce);
    }

    private void Shoot()
    {
        Vector3 direction = (transform.forward).normalized;
        ballScript.ApplyForce(direction * shootForce);
    }

    private void SuperShot()
    {
        Vector3 direction = (transform.forward).normalized;
        ballScript.ApplyForce(direction * superShotForce);
    }

    private void Tackle()
    {
        // Push nearby players
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player") && col.gameObject != gameObject)
            {
                Rigidbody playerRb = col.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    Vector3 direction = (col.transform.position - transform.position).normalized;
                    playerRb.AddForce(direction * 15f, ForceMode.Impulse);
                }
            }
        }
    }

    private void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
        }
    }
}
