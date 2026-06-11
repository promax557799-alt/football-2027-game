using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float friction = 0.98f;
    [SerializeField] private float minVelocity = 0.1f;
    [SerializeField] private float maxVelocity = 40f;
    [SerializeField] private float bounceIntensity = 0.7f;

    private Rigidbody rb;
    private Vector3 lastPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
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

        lastPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            // Bounce effect
            rb.velocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal) * bounceIntensity;
        }
    }

    public void ApplyForce(Vector3 force)
    {
        rb.velocity = force;
    }

    public void ResetPosition(Vector3 position)
    {
        transform.position = position;
        rb.velocity = Vector3.zero;
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
