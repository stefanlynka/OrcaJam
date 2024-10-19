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

    public bool IsAttached = false;

    // Start is called before the first frame update
    void Start()
    {
        //IsAttached = false;

        filter = new ContactFilter2D();
        //filter.SetLayerMask(LayerMask.GetMask("Body")); // Make sure you add a "Glue" layer to objects with the glue tag.
        //filter.useTriggers = true; // If your glue objects use triggers, enable this.

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
            if (col.CompareTag("Body"))
            {
                Debug.Log("Glue has touched a body: " + col.gameObject.name);
                transform.SetParent(col.transform);
                IsAttached = true;
                gameObject.tag = "Body";
                break;
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        
    //}
}
