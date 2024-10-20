using UnityEngine;

public class BlueCell : Cell
{

    private void Start()
    {
        OnStart();
        
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    public override void OnProjectileCollision(Projectile projectile)
    {
        projectile.transform.right *= -1;
    }
}
