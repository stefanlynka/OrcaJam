using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCell : Cell
{
    public ProjectileSpawner ProjectileSpawner;

    private void Start()
    {
        OnStart();
        
    }

    protected override void OnStart()
    {
        base.OnStart();

        ProjectileSpawner.Init(gameObject);
    }
    //private void FireProjectile
}
