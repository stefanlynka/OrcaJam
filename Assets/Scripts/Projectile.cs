using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float direction = 0;

    public virtual void Init(float direction)
    {
        this.direction = direction;
    }

    private void Update()
    {
        DoUpdate();
    }

    protected virtual void DoUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, GameManager.Instance.Player.transform.position);
        if (distanceToPlayer > GameManager.Instance.ProjectileDistanceLimit)
        {
            Destroy(gameObject);
        }
    }

    //private void Update()
    //{
        
    //}
}
