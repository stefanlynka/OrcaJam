using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Health Health;

    // Start is called before the first frame update
    void Start()
    {
        Health.OnProjectileCollision += OnProjectileCollision;
    }

    private void OnProjectileCollision(Projectile projectile)
    {
        Health.ChangeHealth(-projectile.Damage);
    }
}
