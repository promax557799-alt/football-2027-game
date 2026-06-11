using UnityEngine;

public class Setup : MonoBehaviour
{
    void Start()
    {
        CreateGameObjects();
    }

    private void CreateGameObjects()
    {
        // Create Ground
        GameObject ground = new GameObject("Ground");
        ground.tag = "Ground";
        Renderer renderer = ground.AddComponent<MeshFilter>().mesh as Mesh;
        ground.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
        BoxCollider bc = ground.AddComponent<BoxCollider>();
        ground.transform.localScale = new Vector3(100, 1, 80);
        ground.transform.position = new Vector3(0, -1, 0);

        // Create Player
        GameObject player = CreatePlayer(new Vector3(-30, 1, 0), "Player");
        player.tag = "Player";
        player.AddComponent<PlayerMovement>();
        player.AddComponent<BallInteraction>();
        player.layer = LayerMask.NameToLayer("Default");

        // Create AI Players
        for (int i = 0; i < 5; i++)
        {
            GameObject ai = CreatePlayer(new Vector3(20 + i * 5, 1, -15 + i * 5), "AI_" + i);
            ai.tag = "Player";
            ai.layer = LayerMask.NameToLayer("Default");
        }

        // Create Ball
        GameObject ball = new GameObject("Ball");
        ball.AddComponent<SphereCollider>();
        ball.AddComponent<Rigidbody>();
        ball.AddComponent<BallController>();
        ball.transform.position = new Vector3(0, 1, 0);
        ball.transform.localScale = new Vector3(0.22f, 0.22f, 0.22f);
        MeshRenderer ballRenderer = ball.AddComponent<MeshRenderer>();
        MeshFilter ballMeshFilter = ball.AddComponent<MeshFilter>();
        ballRenderer.material = new Material(Shader.Find("Standard"));
        ballRenderer.material.color = new Color(1, 1, 1);

        // Create Goals
        CreateGoal(new Vector3(-50, 0, 0), "PlayerGoal");
        CreateGoal(new Vector3(50, 0, 0), "OpponentGoal");

        Debug.Log("Game Setup Complete!");
    }

    private GameObject CreatePlayer(Vector3 position, string name)
    {
        GameObject player = new GameObject(name);
        player.transform.position = position;

        // Add capsule collider
        CapsuleCollider cc = player.AddComponent<CapsuleCollider>();
        cc.height = 2f;
        cc.radius = 0.3f;

        // Add rigidbody
        Rigidbody rb = player.AddComponent<Rigidbody>();
        rb.mass = 80f;
        rb.drag = 5f;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Add mesh for visualization
        MeshRenderer renderer = player.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = player.AddComponent<MeshFilter>();
        renderer.material = new Material(Shader.Find("Standard"));
        renderer.material.color = Random.ColorHSV();

        return player;
    }

    private void CreateGoal(Vector3 position, string name)
    {
        GameObject goal = new GameObject(name);
        goal.tag = "Goal";
        goal.transform.position = position;

        // Add box collider as trigger
        BoxCollider bc = goal.AddComponent<BoxCollider>();
        bc.size = new Vector3(2, 4, 2);
        bc.isTrigger = true;
    }
}
