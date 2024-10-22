using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : ShotType
{
    public override void Shoot()
    {
        //base.Shoot();
        GameObject obstacle = ObjectPool.instance.GetObstacle();
        obstacle.transform.position = muzzleMain.transform.position;
        obstacle.transform.rotation = muzzleMain.transform.rotation;
        obstacle.SetActive(true);

        projectile.target = prepareShot.currentTarget.transform;
    }
}
