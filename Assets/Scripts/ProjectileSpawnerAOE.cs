using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class ProjectileSpawnerAOE : ProjectileSpawner
{
    public float SpawnCount = 12;



    //public override void Init(GameObject owner)
    //{
    //    base.Init(owner);
    //    //this.RigidBody = rigidBody;
    //}

    protected override void SpawnProjectile()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            float angle = i * (360 / SpawnCount);

            GameObject projectileObject = Instantiate(Prefab);
            projectileObject.transform.position = GetSpawnPosition(angle);
            projectileObject.transform.rotation = Quaternion.Euler(0, 0, angle);

            Projectile projectileComponent = projectileObject.GetComponent<Projectile>();
            projectileComponent?.Init(Owner, angle);
        }
        
    }

    protected Vector2 GetSpawnPosition(float angle)
    {
        // Convert the angle from degrees to radians
        float angleRadians = angle * Mathf.Deg2Rad;

        // Calculate the x and y components using trigonometry
        float x = Mathf.Cos(angleRadians) * spawnDistance;
        float y = Mathf.Sin(angleRadians) * spawnDistance;

        // Return the resulting vector
        return new Vector2(transform.position.x + x, transform.position.y + y); // + new Vector2(x, y);
    }
}
