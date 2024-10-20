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

    void Start()
    {
        filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("PlayerBody")); // Make sure you add a "Glue" layer to objects with the glue tag.
        //filter.useTriggers = true;


        movement = GetComponent<Movement>();

        TimerManager.Instance.AddTimer(new SimpleTimer(CheckForPlayer, gameObject, 1, true));
        Health.OnProjectileCollision += OnProjectileCollision;
        Health.SetOnDeathCallback(OnDeath);
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
        Debug.LogError("Look for Player");

        results.Clear();

        // Check for overlaps with certain colliders
        detectionCollider.OverlapCollider(filter, results);

        foreach (var col in results)
        {
            //Debug.LogError("Found Player");
            Vector2 target = col.transform.position; // Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 self = transform.position;
            Vector2 directionVector = target - self; // Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;

            //Debug.LogError("Angle: " + angle);

            movement.SetTargetRotation(angle);
            break;
            ////Debug.Log("Glue has touched a body: " + col.gameObject.name);
            //transform.SetParent(col.transform);
            ////IsAttached = true;
            //gameObject.tag = "Body";
            //break;
        }
    }

    private void OnProjectileCollision(Projectile projectile)
    {
        Health.ChangeHealth(-projectile.Damage);
    }

    public void OnDeath() {
        //gameObject.SetActive(false);

        SpawnCell();

        Destroy(gameObject);
    }

    private void SpawnCell()
    {
        GameObject cellObject = Instantiate(CellPrefab);
        cellObject.transform.position = transform.position;
    }
}



