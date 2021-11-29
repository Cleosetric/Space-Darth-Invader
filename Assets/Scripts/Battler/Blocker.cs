using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : Enemy
{
    protected override void Start()
    {
        base.Start();
        col2D.enabled = false;
        Invoke("ActiveHostile", 1.5f);
    }

    private void ActiveHostile(){
        col2D.enabled = true;
    }


    protected override void MoveSet()
    {
        //Do Nothing Basically :>
    }

}
