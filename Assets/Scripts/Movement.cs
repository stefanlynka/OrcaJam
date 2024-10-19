using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 1.0f;
    [SerializeField] public float rotationSpeed = 1.0f;
    [SerializeField] public GameObject levelBackground;

    public Vector2 movementDirection = new Vector2(0, 0);
    public float targetRotationAngle = 0.0f;

    private Rigidbody2D rb;
    private SpriteRenderer levelBackgroundRenderer;

    public bool ShouldUpdateRotation = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelBackgroundRenderer = levelBackground.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // apply force
        rb.AddForce(moveSpeed * movementDirection);

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
        Bounds bounds = levelBackgroundRenderer.bounds;
        float radius = bounds.extents.x;
        float distanceFromCenter = rb.position.magnitude;
        return Mathf.Max(distanceFromCenter - radius, 0);

    }
}
