using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : ShotType
{
    public override void Shoot()
    {
        ActivateParticles(muzzleMain);

        GameObject singleProyectile = ObjectPool.instance.GetProyectile();

        GetProjectileMovement(singleProyectile);

        singleProyectile.transform.position = muzzleMain.transform.position;
        singleProyectile.transform.rotation = muzzleMain.transform.rotation;
    }
}
