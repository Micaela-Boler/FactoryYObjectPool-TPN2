using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultShot : ShotType
{
    private Transform lockOnPos;

    public override void Shoot()
    {
        lockOnPos = prepareShot.currentTarget.transform;

        //Aplicar POOL OBJECT
        base.Shoot() ;
        projectile.target = lockOnPos;
    }
}
