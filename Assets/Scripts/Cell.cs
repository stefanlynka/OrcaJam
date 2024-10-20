using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;
using Color = UnityEngine.Color;

public enum Faction
{
    Player,
    Enemy,
    Neutral
}

public class Cell : MonoBehaviour
{
    public Faction Faction = Faction.Neutral;

    public Collider2D BodyCollider;
    public Collider2D GlueCollider;
    public Rigidbody2D RigidBody;
    public ContactFilter2D filter;      // Filter to specify what layers/tags to track

    private List<Collider2D> results = new List<Collider2D>();  // To store results each frame
    private Health health;
    private SpriteRenderer spriteRenderer;

    public bool IsAttached = false;
    public bool IsDisabled = false;
    public bool IsAttachedToPlayer = false;

    // Define two colors to interpolate between
    public Color DefaultColor = new Color(255, 255, 255);
    public Color FadedColor = new Color(150, 150, 150);
    public float FadeSpeed = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
        filter = new ContactFilter2D();

        spriteRenderer = GetComponent<SpriteRenderer>();

        health = GetComponent<Health>();
        health.SetOnDeathCallback(OnDeath);

        health.OnProjectileCollision += OnProjectileCollision;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollisions();
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (IsAttached) return;

        // Calculate the sine wave value (-1 to 1)
        float t = Mathf.Sin(Time.time * FadeSpeed);

        // Normalize sine wave value to range from 0 to 1
        float normalizedT = (t + 1f) / 2f;

        // Interpolate between colorA and colorB
        spriteRenderer.color = Color.Lerp(DefaultColor, FadedColor, normalizedT);
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
            if (col.CompareTag("Body") && !IsDisabled)
            {
                //Debug.Log("Glue has touched a body: " + col.gameObject.name);
                transform.SetParent(col.transform);
                IsAttached = true;
                gameObject.tag = "Body";
                spriteRenderer.color = DefaultColor;

                if (col.GetComponentInParent<Player>() != null)
                {
                    GameManager.Instance.ChangeAttachedCells(1);
                    IsAttachedToPlayer = true;
                }
                gameObject.layer = col.gameObject.layer;//LayerMask.NameToLayer("PlayerBody");
                break;
            }
        }
    }

    private void OnDeath()
    {
        SetIsDisabled(true);
        transform.SetParent(null);

        gameObject.tag = "Untagged";

        Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody == null)
        {
            rigidBody = gameObject.AddComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0;
            rigidBody.angularDrag = 0.6f;
            rigidBody.drag = 3;
        }
    }


    private void SetIsDisabled(bool isDisabled)
    {
        IsDisabled = isDisabled;

        if (isDisabled)
        {

            health.SetHealthBarVisiblity(false);
            if (IsAttachedToPlayer) GameManager.Instance.ChangeAttachedCells(-1);

            // set tint to darker
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1);

            foreach (Transform childTransform in transform)
            {
                Cell child = childTransform.GetComponentInChildren<Cell>();
                TimerManager.Instance.AddTimer(new SimpleTimer(() =>
                {
                    child?.SetIsDisabled(true);
                }, gameObject, 0.5f, false));
            }
        }
    }

    private void OnProjectileCollision(Projectile projectile)
    {
        if (!IsDisabled) health.ChangeHealth(-projectile.Damage);
    }

}
