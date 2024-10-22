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
            //Aplicar POOL OBJECT
            base.Shoot();
            projectile.target = transform.GetComponent<PrepareShot>().currentTarget.transform;
        }
        else
        {
            //Aplicar POOL OBJECT
            Instantiate(muzzleEff, muzzleSub.transform.position, muzzleSub.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleSub.transform.position, muzzleSub.rotation);
            Projectile projectile = missleGo.GetComponent<Projectile>(); //REPETIDO
            projectile.target = transform.GetComponent<PrepareShot>().currentTarget.transform;
        }

        shootLeft = !shootLeft;
        
    }
}
