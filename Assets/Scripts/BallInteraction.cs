using UnityEngine;

public class BallInteraction : MonoBehaviour
{
    [Header("Ball Interaction")]
    [SerializeField] private float passForce = 20f;
    [SerializeField] private float shootForce = 30f;
    [SerializeField] private float superShotForce = 50f;
    [SerializeField] private float ballInteractionRadius = 2f;
    [SerializeField] private float tackleForce = 15f;

    private BallController ballScript;
    private InputManager inputManager;
    private PlayerMovement playerMovement;

    void Start()
    {
        ballScript = FindObjectOfType<BallController>();
        inputManager = InputManager.instance;
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (ballScript == null) return;

        float distanceToBall = Vector3.Distance(transform.position, ballScript.transform.position);
        
        if (distanceToBall < ballInteractionRadius)
        {
            HandleBallInteraction();
        }
    }

    private void HandleBallInteraction()
    {
        if (inputManager.GetPassInput())
        {
            Pass();
        }
        if (inputManager.GetShootInput())
        {
            Shoot();
        }
        if (inputManager.GetSuperShotInput())
        {
            SuperShot();
        }
        if (inputManager.GetTackleInput())
        {
            Tackle();
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
                    playerRb.AddForce(direction * tackleForce, ForceMode.Impulse);
                }
            }
        }
    }
}
