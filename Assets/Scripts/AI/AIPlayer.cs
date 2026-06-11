using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float stoppingDistance = 5f;
    [SerializeField] private float shootDistance = 15f;
    [SerializeField] private float passForce = 20f;
    [SerializeField] private float shootForce = 25f;

    private Rigidbody rb;
    private Ball ballScript;
    private Transform targetPosition;
    private bool hasTarget = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        ballScript = FindObjectOfType<Ball>();
        SetRandomPatrolPosition();
    }

    void Update()
    {
        if (ballScript == null) return;

        float distanceToBall = Vector3.Distance(transform.position, ballScript.transform.position);
        
        // If near ball, try to shoot or pass
        if (distanceToBall < stoppingDistance)
        {
            if (distanceToBall < shootDistance)
            {
                TryToShoot();
            }
            else
            {
                TryToPass();
            }
        }
        else
        {
            // Move towards ball
            MoveTowards(ballScript.transform.position);
        }
    }

    void FixedUpdate()
    {
        if (hasTarget)
        {
            MovePlayer();
        }
    }

    private void MoveTowards(Vector3 position)
    {
        targetPosition = null;
        hasTarget = true;
        
        Vector3 direction = (position - transform.position).normalized;
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
        
        // Look at target
        transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
    }

    private void MovePlayer()
    {
        if (targetPosition == null) return;
        
        Vector3 direction = (targetPosition.position - transform.position).normalized;
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
    }

    private void TryToShoot()
    {
        if (ballScript != null)
        {
            Vector3 goalPosition = transform.position.x > 0 ? new Vector3(100, 0, 0) : new Vector3(-100, 0, 0);
            Vector3 shootDirection = (goalPosition - ballScript.transform.position).normalized;
            ballScript.ApplyForce(shootDirection * shootForce);
        }
    }

    private void TryToPass()
    {
        if (ballScript != null)
        {
            Vector3 direction = transform.forward;
            ballScript.ApplyForce(direction * passForce);
        }
    }

    private void SetRandomPatrolPosition()
    {
        Vector3 randomPos = new Vector3(Random.Range(-30f, 30f), 0, Random.Range(-40f, 40f));
        targetPosition = new GameObject("PatrolTarget").transform;
        targetPosition.position = randomPos;
    }
}
