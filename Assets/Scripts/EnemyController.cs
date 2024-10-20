using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Movement movement;

    public Collider2D detectionCollider;
    private List<Collider2D> results = new List<Collider2D>();
    private List<Collider2D> projectileResults = new List<Collider2D>();
    public ContactFilter2D filter;      // Filter to specify what layers/tags to track
    public Health Health;

    public GameObject CellPrefab;
    private GameObject playerTarget;

    public float minAcceptableDistanceToPlayer = 0;
    public float maxAcceptableDistanceToPlayer = 3;

    void Start()
    {
        filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("PlayerBody")); // Make sure you add a "Glue" layer to objects with the glue tag.

        movement = GetComponent<Movement>();

        TimerManager.Instance.AddTimer(new SimpleTimer(CheckForPlayer, gameObject, 1, true));
        Health.OnProjectileCollision += OnProjectileCollision;
        Health.SetOnDeathCallback(OnDeath);

        TimerManager.Instance.AddTimer(new SimpleTimer(FindWanderDirection, gameObject, 5, true));

        TimerManager.Instance.AddTimer(new SimpleTimer(TryMoveTowardsPlayer, gameObject, 1, true));
        
    }

    void Update()
    {
        //// set movement based on WASD
        //movement.SetMoveDirection(new Vector2(
        //    Input.GetAxis("Horizontal"),
        //    Input.GetAxis("Vertical")
        //    )
        //);

        // set target rotation angle based on mouse location
        // Vector2 directionToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        // movement.SetTargetRotation(angle);

    }

    private void CheckForPlayer()
    {
        results.Clear();

        // Check for overlaps with certain colliders
        detectionCollider.OverlapCollider(filter, results);

        bool foundTarget = false;
        foreach (var col in results)
        {
            playerTarget = col.gameObject;
            foundTarget = true;

            Vector2 target = col.transform.position; 
            Vector2 self = transform.position;
            Vector2 directionVector = target - self;
            float angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;

            movement.SetTargetRotation(angle);
            break;
        }

        if (!foundTarget) playerTarget = null;
    }

    private void OnProjectileCollision(Projectile projectile)
    {
        Health.ChangeHealth(-projectile.Damage);
    }

    public void OnDeath() {
        SpawnCell();

        Destroy(gameObject);
    }

    private void SpawnCell()
    {
        GameObject cellObject = Instantiate(CellPrefab);
        cellObject.transform.position = transform.position;
    }

    private void FindWanderDirection()
    {
        if (playerTarget != null) return;

        Vector2 wanderDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        movement.SetMoveDirection(wanderDirection);

        //Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y) + wanderDirection;
        Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y) + new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));

        LookTowards(targetPosition);
    }

    private float GetAngleTowards(Vector2 targetPosition)
    {
        Vector2 target = targetPosition; // Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 self = transform.position;
        Vector2 directionVector = target - self; // Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        return Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
    }

    private void LookTowards(Vector2 targetPosition)
    {
        Vector2 target = targetPosition; // Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 self = transform.position;
        Vector2 directionVector = target - self; // Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;

        movement.SetTargetRotation(angle);
    }

    private void TryMoveTowardsPlayer()
    {
        if (playerTarget == null) return;

        float distance = Vector2.Distance(playerTarget.transform.position, transform.position);
        if (distance > minAcceptableDistanceToPlayer && distance < maxAcceptableDistanceToPlayer)
        {
            movement.SetMoveDirection(new Vector2());
            return;
        }

        Vector2 direction = playerTarget.transform.position - transform.position;
        direction.Normalize();
        direction = ShiftDirection(direction, 60);
        if (distance < minAcceptableDistanceToPlayer) direction *= -1;
        //if (distance < 5)
        //float angle = GetAngleTowards(playerTarget.transform.position);
        movement.SetMoveDirection(direction);
    }

    public Vector2 ShiftDirection(Vector2 originalDirection, float maxAngle)
    {
        // Get the current angle of the vector in degrees
        float currentAngle = Mathf.Atan2(originalDirection.y, originalDirection.x) * Mathf.Rad2Deg;

        // Generate a random angle between -maxAngle and +maxAngle
        float randomAngle = UnityEngine.Random.Range(-maxAngle, maxAngle);

        // Apply the random angle shift
        float newAngle = currentAngle + randomAngle;

        // Convert the new angle back to a direction (Vector2)
        float newAngleRad = newAngle * Mathf.Deg2Rad;
        Vector2 newDirection = new Vector2(Mathf.Cos(newAngleRad), Mathf.Sin(newAngleRad));

        return newDirection;
    }

}



