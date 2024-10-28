using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualShot : ShotType
{
    private bool shootLeft = true;
    [SerializeField] private Transform muzzleSub;
   

    public override void Shoot()
    {
        if (shootLeft)
        {
            base.Shoot();
        }
        else
        {
            ActivateParticles(muzzleSub);

            GameObject missleGo = Instantiate(bullet, muzzleSub.transform.position, muzzleSub.rotation);
            GetProjectileMovement(missleGo);
        }

        shootLeft = !shootLeft;
        
    }
}
