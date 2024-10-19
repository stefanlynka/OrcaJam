using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 1.0f;
    [SerializeField] public float rotationSpeed = 1.0f;

    public Vector2 movementDirection = new Vector2(0, 0);
    public float targetRotationAngle = 0.0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // apply force
        rb.AddForce(moveSpeed * movementDirection);

        // rotate toward target angle
        // Quaternion targetRotation = Quaternion.AngleAxis(targetRotationAngle, Vector3.forward);
        // transform.rotation = Quaternion.Slerp(
        //     transform.rotation,
        //     targetRotation,
        //     rotationSpeed * Time.deltaTime
        // );
        // transform.rotation = Quaternion.RotateTowards(
        //     transform.rotation,
        //     targetRotation,
        //     rotationSpeed * Time.deltaTime
        // );
    }

    public void SetMoveDirection(Vector2 newDirection)
    {
        this.movementDirection = newDirection;
    }

    public void SetTargetRotation(float newTargetRotationAngle)
    {
        this.targetRotationAngle = newTargetRotationAngle;
    }
}
