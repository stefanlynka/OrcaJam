using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject prefab;

    public float spawnInterval = 5f;

    public float spawnDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        TimerManager.Instance.AddTimer(new SimpleTimer(SpawnProjectile, spawnInterval, true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(prefab);
        newProjectile.transform.position = GetSpawnPosition();
        newProjectile.transform.rotation = transform.rotation;

        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        float rotation = transform.rotation.eulerAngles.z;
        projectileComponent?.Init(rotation);
        //if (projectileComponent != null) 
    }

    protected virtual Vector2 GetSpawnPosition()
    {
        // Get the player's current position
        Vector2 playerPosition = transform.position;
        // Get the direction the player is facing (right vector in 2D)
        Vector2 forwardDirection = transform.right;

        Vector2 targetPosition = playerPosition + forwardDirection * spawnDistance;

        return targetPosition;

    }
}
