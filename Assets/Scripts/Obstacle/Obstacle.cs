using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField] BoxCollider2D boxCollider;

    private List<GameObject> collidingObjects = new List<GameObject>();

    void LateUpdate()
    {
        foreach (GameObject collidingObject in collidingObjects) {
            if (!collidingObject.CompareTag("Body")) continue;
            Health health = collidingObject.GetComponent<Health>();
            if (health == null) continue;
            health.ChangeHealth(-25 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Body")) {
            collidingObjects.Add(collision.collider.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Body")) {
            collidingObjects.Remove(collision.collider.gameObject);
        }
    }
}
