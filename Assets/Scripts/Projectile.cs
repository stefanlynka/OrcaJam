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

    public SpriteRenderer SpriteRenderer;
    public Sprite PlayerProjectile;
    public Sprite EnemyProjectile;

    public GameObject Owner;

    public virtual void Init(GameObject Owner, float direction)
    {
        this.direction = direction;
        this.Owner = Owner;


        UpdateFilter();
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
            if (col.gameObject == Owner) continue;

            Health health = col.GetComponent<Health>();
            if (health != null)
            {
                health.ProjectileCollision(this);
            }

            Destroy(gameObject);
            break;
        }
    }

    private void UpdateFilter()
    {
        filter = new ContactFilter2D();

        if (Owner.layer == LayerMask.NameToLayer("PlayerBody"))
        {
            // Define the layers you want to exclude
            int bodyLayer = LayerMask.NameToLayer("PlayerBody");
            int neutralLayer = LayerMask.NameToLayer("NeutralBody");
            // Create a LayerMask that excludes those layers
            LayerMask excludedLayers = (1 << bodyLayer) | (1 << neutralLayer);
            // Invert the LayerMask to include all layers except the excluded ones
            LayerMask includedLayers = ~excludedLayers;
            // Apply the LayerMask to the filter, only allowing detection on layers except the excluded ones
            filter.SetLayerMask(includedLayers);
        }
        else if (Owner.layer == LayerMask.NameToLayer("EnemyBody"))
        {
            // Define the layers you want to exclude
            int bodyLayer = LayerMask.NameToLayer("EnemyBody");
            int neutralLayer = LayerMask.NameToLayer("NeutralBody");
            // Create a LayerMask that excludes those layers
            LayerMask excludedLayers = (1 << bodyLayer) | (1 << neutralLayer);
            // Invert the LayerMask to include all layers except the excluded ones
            LayerMask includedLayers = ~excludedLayers;
            // Apply the LayerMask to the filter, only allowing detection on layers except the excluded ones
            filter.SetLayerMask(includedLayers);
        }
        else if (Owner.layer == LayerMask.NameToLayer("NeutralBody"))
        {
            // Define the layers you want to exclude
            int neutralLayer = LayerMask.NameToLayer("NeutralBody");
            // Create a LayerMask that excludes those layers
            LayerMask excludedLayers = (1 << neutralLayer);
            // Invert the LayerMask to include all layers except the excluded ones
            LayerMask includedLayers = ~excludedLayers;
            // Apply the LayerMask to the filter, only allowing detection on layers except the excluded ones
            filter.SetLayerMask(includedLayers);
        }
    }
}
