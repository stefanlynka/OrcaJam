using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

public class Cell : MonoBehaviour
{

    public Collider2D bodyCollider;
    public Collider2D glueCollider;
    public ContactFilter2D filter;      // Filter to specify what layers/tags to track

    private List<Collider2D> results = new List<Collider2D>();  // To store results each frame
    private Health health;
    private SpriteRenderer spriteRenderer;

    public bool IsAttached = false;
    public bool IsDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        //IsAttached = false;

        filter = new ContactFilter2D();
        //filter.SetLayerMask(LayerMask.GetMask("Body")); // Make sure you add a "Glue" layer to objects with the glue tag.
        //filter.useTriggers = true; // If your glue objects use triggers, enable this.

        spriteRenderer = GetComponent<SpriteRenderer>();

        health = GetComponent<Health>();
        health.SetOnDeathCallback(OnDeath);
    }

    // Update is called once per frame
    void Update()
    {
        //bodyCollider.

        CheckCollisions();
    }

    private void CheckCollisions()
    {
        if (IsAttached) return;

        results.Clear();

        // Check for overlaps with certain colliders
        glueCollider.OverlapCollider(filter, results);

        foreach (var col in results)
        {
            // Check if the collider has the "Body" tag
            if (col.CompareTag("Body") && !IsDisabled)
            {
                Debug.Log("Glue has touched a body: " + col.gameObject.name);
                transform.SetParent(col.transform);
                IsAttached = true;
                gameObject.tag = "Body";
                break;
            }
        }
    }

    private void OnDeath()
    {

        transform.SetParent(null);
        SetIsDisabled(true);

    }

    private void SetIsDisabled(bool isDisabled)
    {
        IsDisabled = isDisabled;
        health.SetHealth(0);
        if (isDisabled)
        {
            // set tint to darker
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1);

            foreach (Transform childTransform in transform)
            {
                Cell child = childTransform.GetComponentInChildren<Cell>();
                TimerManager.Instance.AddTimer(new SimpleTimer(() =>
                {
                    child?.SetIsDisabled(true);
                }, 0.5f, false));
            }

            // TimerManager.Instance.AddTimer(new SimpleTimer(() =>
            // {
            //     childCell?.SetIsDisabled(true);
            //     print(childCell?.IsDisabled);
            // }, 0.25f, false));
        }
        else
        {

        }
    }

}
