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
        Health.SetOnDeathCallback(OnDeath);
    }

    private void OnProjectileCollision(Projectile projectile)
    {
        Health.ChangeHealth(-projectile.Damage);
    }

    private void OnDeath() {
        GameManager.Instance.SetIsDeadState(true);
    }
}
