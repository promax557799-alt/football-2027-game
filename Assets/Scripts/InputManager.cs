using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GetMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        return new Vector3(horizontal, 0, vertical);
    }

    public bool GetPassInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool GetShootInput()
    {
        return Input.GetMouseButtonDown(1);
    }

    public bool GetSuperShotInput()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }

    public bool GetTackleInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public bool GetJumpInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetSprintInput()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool GetPauseInput()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
