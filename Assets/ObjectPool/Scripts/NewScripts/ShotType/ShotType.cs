using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShotType : MonoBehaviour
{
    //[SerializeField] private List<GameObject> pooledObstacles;
    public GameObject muzzleEff;
    public GameObject bullet;

    [Header("OTHER SCRIPTS")]
    public PrepareShot prepareShot;
    protected Projectile projectile;

    [Header("MUZZLE TRANSFORM")]
    public Transform muzzleMain;



   // private void Start()
    //{
    //    ObjectPool.instance.AddProyectileToPool(bullet,pooledObstacles);
    //}

    public virtual void Shoot()
    {
        //GameObject obstacle = ObjectPool.instance.GetObstacle();
        //obstacle.transform.position = muzzleMain.transform.position;
        //obstacle.SetActive(true);

        Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
        GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
        projectile = missleGo.GetComponent<Projectile>();
    }
}
