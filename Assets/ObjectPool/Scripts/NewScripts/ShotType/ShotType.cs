using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShotType : MonoBehaviour
{
    public GameObject muzzleEff;
    public GameObject bullet;

    [Header("OTHER SCRIPTS")]
    public FindTarget findTarget;
    protected ProjectileMovement projectileMovement;

    [Header("MUZZLE TRANSFORM")]
    public Transform muzzleMain;



    public virtual void Shoot()
    {
        GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
        GetProjectileMovement(missleGo);
        ActivateParticles(muzzleMain);
    }

    protected virtual void GetProjectileMovement(GameObject bullet)
    {
        projectileMovement = bullet.GetComponent<ProjectileMovement>();
        projectileMovement.target = findTarget.currentTarget.transform;
    }

    protected virtual void ActivateParticles(Transform muzzleTransform)
    {
        Instantiate(muzzleEff, muzzleTransform.transform.position, muzzleTransform.rotation);
    }
}
