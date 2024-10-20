using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

public abstract class Projectile : MonoBehaviour
{
    protected float direction = 0;
    public float Damage = 0;

    public Collider2D Collider;
    public ContactFilter2D filter;      // Filter to specify what layers/tags to track
    private List<Collider2D> results = new List<Collider2D>();  // To store results each frame

    public GameObject Owner;

    public virtual void Init(GameObject Owner, float direction)
    {
        this.direction = direction;
        this.Owner = Owner;

        filter = new ContactFilter2D();
        //filter.SetLayerMask(LayerMask.GetMask("Body")); // Make sure you add a "Glue" layer to objects with the glue tag.
        //filter.useTriggers = true; // If your glue objects use triggers, enable this.

        //TimerManager.Instance.AddTimer(new SimpleTimer())
    }

    private void Update()
    {
        DoUpdate();

    }

    protected virtual void DoUpdate()
    {
        CheckCollisions();

        float distanceToPlayer = Vector2.Distance(transform.position, GameManager.Instance.Player.transform.position);
        if (distanceToPlayer > GameManager.Instance.ProjectileDistanceLimit)
        {
            Destroy(gameObject);
        }
    }

    private void CheckCollisions()
    {
        results.Clear();

        // Check for overlaps with certain colliders
        Collider.OverlapCollider(filter, results);

        foreach (var col in results)
        {
            // Check if the collider has the "Body" tag
            if (col.CompareTag("Body"))
            {
                if (col.gameObject == Owner) continue;

                Debug.Log("Projectile touched a body: " + col.gameObject.name);
                Health health = col.GetComponent<Health>();
                if (health != null)
                {
                    health.ProjectileCollision(this);
                }

                Destroy(gameObject);
                break;
            }
        }
    }

    //private void Update()
    //{
        
    //}
}
