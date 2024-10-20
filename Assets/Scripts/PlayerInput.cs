using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private Movement movement;
    public bool InputEnabled = true;

    void Start()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (!InputEnabled)
        {
            movement.SetMoveDirection(new Vector2(0, 0));
        }
        else
        {
            // set movement based on WASD
            movement.SetMoveDirection(new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
                )
            );
        }

        // set target rotation angle based on mouse location
        // Vector2 directionToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        // movement.SetTargetRotation(angle);

    }

    public void SetInputEnabled(bool enabled)
    {
        InputEnabled = enabled;
    }
}
