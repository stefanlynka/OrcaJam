using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 1.0f;
    [SerializeField] public float rotationSpeed = 1.0f;

    public Vector2 movementDirection = new Vector2(0, 0);
    public float targetRotationAngle = 0.0f;

    private Rigidbody2D rb;

    public bool ShouldUpdateRotation = false;
    public bool EnhancedAcceleration = false;
    public float EnhancedAccelerationModifier = 1.5f;
    public float EnhancementThreshold = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // apply force
        Vector2 movementVector = movementDirection;
        if (EnhancedAcceleration)
        {
            //if (movementVector.x > 0 && rb.velocity.x < )
            if (rb.velocity.x * movementVector.x < 0 || Mathf.Abs(rb.velocity.x) < EnhancementThreshold)
            {
                //Debug.LogError("Boost X");
                movementVector.x *= EnhancedAccelerationModifier;
            }
            if (rb.velocity.y * movementVector.y < 0 || Mathf.Abs(rb.velocity.y) < EnhancementThreshold)
            {
                //Debug.LogError("Boost Y");
                movementVector.y *= EnhancedAccelerationModifier;
            }
            //movementVector
        }
        rb.AddForce(moveSpeed * movementVector);

        if (ShouldUpdateRotation)
        {
             //rotate toward target angle
            Quaternion targetRotation = Quaternion.AngleAxis(targetRotationAngle, Vector3.forward);
            //transform.rotation = Quaternion.Slerp(
            //    transform.rotation,
            //    targetRotation,
            //    rotationSpeed * Time.deltaTime
            //);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    void LateUpdate()
    {
        float distPastBounds = GetDistancePastLevelBounds();
        if (distPastBounds > 0) {
            Vector2 directionToCenter = Vector2.zero - rb.position;
            rb.AddForce(moveSpeed * distPastBounds/100 * directionToCenter);
        }
    }

    public void SetMoveDirection(Vector2 newDirection)
    {
        this.movementDirection = newDirection;
    }

    public void SetTargetRotation(float newTargetRotationAngle)
    {
        this.targetRotationAngle = newTargetRotationAngle;
    }

    public float GetDistancePastLevelBounds()
    {
        Bounds bounds = GameManager.Instance.GetLevelRenderer().bounds;
        float radius = bounds.extents.x;
        float distanceFromCenter = rb.position.magnitude;
        return Mathf.Max(distanceFromCenter - radius, 0);

    }
}
