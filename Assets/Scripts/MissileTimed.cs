using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTimed : Missile
{
    public float Duration = 2;


    public override void Init(GameObject Owner, float direction)
    {
        base.Init(Owner, direction);


        if (Owner.layer == LayerMask.NameToLayer("PlayerBody"))
        {
            if (PlayerSpeedMod != 0) Duration /= PlayerSpeedMod;
        }

        TimerManager.Instance.AddTimer(new SimpleTimer(Remove, gameObject, Duration, false));
    }


    //// Update is called once per frame
    //protected override void DoUpdate()
    //{
    //    base.DoUpdate();
    //    transform.position += transform.right * Speed * Time.deltaTime;
    //}

    private void Remove()
    {
        Destroy(gameObject);
    }
}
