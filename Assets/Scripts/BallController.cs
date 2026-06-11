using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float friction = 0.98f;
    [SerializeField] private float minVelocity = 0.1f;
    [SerializeField] private float maxVelocity = 40f;
    [SerializeField] private float bounceIntensity = 0.7f;
    [SerializeField] private float rollResistance = 0.95f;

    private Rigidbody rb;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = 0.43f; // Standard football mass in kg
            rb.drag = 0.1f;
            rb.angularDrag = 0.05f;
        }
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Apply friction
        rb.velocity *= friction;

        // Stop if velocity is too low
        if (rb.velocity.magnitude < minVelocity)
        {
            rb.velocity = Vector3.zero;
        }

        // Limit max velocity
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }

        // Ground friction
        if (transform.position.y < 0.5f)
        {
            rb.velocity *= rollResistance;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            if (collision.contacts.Length > 0)
            {
                rb.velocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal) * bounceIntensity;
            }
        }
    }

    public void ApplyForce(Vector3 force)
    {
        rb.velocity = force;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    public void SetStartPosition(Vector3 position)
    {
        startPosition = position;
    }
}
