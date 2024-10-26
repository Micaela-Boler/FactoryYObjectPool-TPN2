using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultShot : ShotType
{
    private Transform lockOnPos;

    public override void Shoot()
    {
        lockOnPos = prepareShot.currentTarget.transform;

        base.Shoot() ;
        projectileMovement.target = lockOnPos;
    }
}
