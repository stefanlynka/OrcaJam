using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Movement movement;

    public Collider2D detectionCollider;
    private List<Collider2D> results = new List<Collider2D>();
    public ContactFilter2D filter;      // Filter to specify what layers/tags to track


    void Start()
    {
        filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("PlayerBody")); // Make sure you add a "Glue" layer to objects with the glue tag.
        //filter.useTriggers = true;


        movement = GetComponent<Movement>();

        TimerManager.Instance.AddTimer(new SimpleTimer(CheckForPlayer, 1, true));
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

            movement.SetTargetRotation(angle);
            break;
        }
    }

    //private void UpdateFilter()
    //{
    //    filter = new ContactFilter2D();

    //    if (Owner.layer == LayerMask.NameToLayer("PlayerBody"))
    //    {
    //        // Define the layers you want to exclude
    //        int bodyLayer = LayerMask.NameToLayer("PlayerBody");
    //        int neutralLayer = LayerMask.NameToLayer("NeutralBody");
    //        // Create a LayerMask that excludes those layers
    //        LayerMask excludedLayers = (1 << bodyLayer) | (1 << neutralLayer);
    //        // Invert the LayerMask to include all layers except the excluded ones
    //        LayerMask includedLayers = ~excludedLayers;
    //        // Apply the LayerMask to the filter, only allowing detection on layers except the excluded ones
    //        filter.SetLayerMask(includedLayers);
    //    }
    //    else if (Owner.layer == LayerMask.NameToLayer("EnemyBody"))
    //    {
    //        // Define the layers you want to exclude
    //        int bodyLayer = LayerMask.NameToLayer("EnemyBody");
    //        int neutralLayer = LayerMask.NameToLayer("NeutralBody");
    //        // Create a LayerMask that excludes those layers
    //        LayerMask excludedLayers = (1 << bodyLayer) | (1 << neutralLayer);
    //        // Invert the LayerMask to include all layers except the excluded ones
    //        LayerMask includedLayers = ~excludedLayers;
    //        // Apply the LayerMask to the filter, only allowing detection on layers except the excluded ones
    //        filter.SetLayerMask(includedLayers);
    //    }
    //    else if (Owner.layer == LayerMask.NameToLayer("NeutralBody"))
    //    {
    //        // Define the layers you want to exclude
    //        int neutralLayer = LayerMask.NameToLayer("NeutralBody");
    //        // Create a LayerMask that excludes those layers
    //        LayerMask excludedLayers = (1 << neutralLayer);
    //        // Invert the LayerMask to include all layers except the excluded ones
    //        LayerMask includedLayers = ~excludedLayers;
    //        // Apply the LayerMask to the filter, only allowing detection on layers except the excluded ones
    //        filter.SetLayerMask(includedLayers);
    //    }
    //}
}



