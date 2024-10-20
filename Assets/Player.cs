using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Health HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.OnProjectileCollision += OnProjectileCollision;
    }

    private void OnProjectileCollision(Projectile projectile)
    {
        HealthBar.ChangeHealth(projectile.Damage);
    }
}
