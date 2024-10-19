using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    public float Speed = 0;
    public float Rotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Init(float direction)
    {
        this.direction = direction;
        //transform.rot
        transform.rotation = Quaternion.AngleAxis(Rotation + direction, Vector3.forward);
    }


    // Update is called once per frame
    protected override void DoUpdate()
    {
        base.DoUpdate();
        //if (transform.position.magnitude > )
        transform.position += transform.right * Speed * Time.deltaTime;
    }

}
