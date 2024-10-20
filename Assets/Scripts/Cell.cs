using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

public class Cell : MonoBehaviour
{
    public Collider2D BodyCollider;
    public Collider2D GlueCollider;
    public Rigidbody2D RigidBody;
    public ContactFilter2D filter;      // Filter to specify what layers/tags to track

    private List<Collider2D> results = new List<Collider2D>();  // To store results each frame
    private Health health;

    public bool IsAttached = false;
    public bool IsDisabled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
        filter = new ContactFilter2D();

        health = GetComponent<Health>();
        health.SetOnDeathCallback(OnDeath);

        health.OnProjectileCollision += OnProjectileCollision;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        if (IsAttached) return;

        results.Clear();

        // Check for overlaps with certain colliders
        GlueCollider.OverlapCollider(filter, results);

        foreach (var col in results)
        {
            // Check if the collider has the "Body" tag
            if (col.CompareTag("Body"))
            {
                Debug.Log("Glue has touched a body: " + col.gameObject.name);
                transform.SetParent(col.transform);
                //RigidBody.
                IsAttached = true;
                gameObject.tag = "Body";
                break;
            }
        }
    }

    private void OnDeath() {

        transform.SetParent(null);

        Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody == null)
        {
            rigidBody = gameObject.AddComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0;
            rigidBody.angularDrag = 0.6f;
            rigidBody.drag = 3;
        }


        // Cell childCell = GetComponentInChildren<Cell>();


    }

    private void OnProjectileCollision(Projectile projectile)
    {
        health.ChangeHealth(-projectile.Damage);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //}
}
