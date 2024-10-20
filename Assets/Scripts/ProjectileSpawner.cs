using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject Owner;
    public Rigidbody2D RigidBody;

    public float spawnInterval = 5f;

    public float spawnDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        TimerManager.Instance.AddTimer(new SimpleTimer(SpawnProjectile, gameObject, spawnInterval, true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(GameObject owner)
    {
        this.Owner = owner;
        //this.RigidBody = rigidBody;
    }

    private void SpawnProjectile()
    {
        GameObject projectileObject = Instantiate(Prefab);
        projectileObject.transform.position = GetSpawnPosition();
        projectileObject.transform.rotation = transform.rotation;

        Projectile projectileComponent = projectileObject.GetComponent<Projectile>();
        float rotation = transform.rotation.eulerAngles.z;

        projectileComponent?.Init(Owner, rotation);
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
