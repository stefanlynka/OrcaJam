using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    public float Speed = 0;
    public float Rotation = 0;
    public float PlayerSpeedMod = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Init(GameObject Owner, float direction)
    {
        base.Init(Owner, direction);

        this.direction = direction;
        transform.rotation = Quaternion.AngleAxis(Rotation + direction, Vector3.forward);

        if (Owner.layer == LayerMask.NameToLayer("PlayerBody"))
        {
            Speed *= PlayerSpeedMod;
        }
    }


    // Update is called once per frame
    protected override void DoUpdate()
    {
        base.DoUpdate();
        transform.position += transform.right * Speed * Time.deltaTime;
    }

}
