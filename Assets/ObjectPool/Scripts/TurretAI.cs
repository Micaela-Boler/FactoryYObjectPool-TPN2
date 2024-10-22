using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    public enum TurretType
    {
        Single = 1,
        Dual = 2,
        Catapult = 3,
    }
    
    public GameObject currentTarget; // //---
    public Transform turreyHead; //-----

    public float attackDist = 10.0f; // // -----
    //public float attackDamage;
    public float shootCoolDown; //
    private float timer; //
    public float loockSpeed; //----

    public Vector3 randomRot;  //------------
    public Animator animator; //

    [Header("[Turret Type]")]
    public TurretType turretType = TurretType.Single;
    
    public Transform muzzleMain;
    public Transform muzzleSub;
    public GameObject muzzleEff;
    public GameObject bullet;
    private bool shootLeft = true;

    private Transform lockOnPos;


    //HACER CLASE PARA LA DETECCION Y OTRO PARA EL TIPO DE TIRO

    void Start () {

        InvokeRepeating("CheckForTarget", 0, 0.5f);


        if (transform.GetChild(0).GetComponent<Animator>())
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }


        randomRot = new Vector3(0, Random.Range(0, 359), 0); //ROTAR
    }
	

	void Update () 
    {
        // SI EL JUGADOR NO ES NULO, SIGUE LA POSICION DE ESTE
        //CHEQUEA SI EL JUGADOR ESTA DENTRO DEL RANGO, SI NO LO ESTA ENTONCES ROTA, SI ESTA LO SIGUE
        if (currentTarget != null) //SI ESTA ES EL RANGO LO SIGUE
        {
            FollowTarget();

            float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (currentTargetDist > attackDist) //SI ESTA NO ESTA EN LA DISTANCIA DE ATAQUE ES NULO
            {
                currentTarget = null;
            }
        }
        else
        {
            IdleRotate();
        }


        //COOLDOWN DE DISPARO Y ANIMACION
        timer += Time.deltaTime;
        if (timer >= shootCoolDown)
        {
            if (currentTarget != null)
            {
                timer = 0;
                
                if (animator != null)
                {
                    animator.SetTrigger("Fire");
                    ShootTrigger();
                }
                else
                {
                    ShootTrigger();
                }
            }
        }
	}

    //DETENTA SI EL JUGADOR ESTA EN EL RANGO DE DISPARO
    //SE LLAMA CONSTANTEMENTE EN EL START
    private void CheckForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Player")
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }

    //CUANDO EL JUGADOR NO ES NULO, LA CABEZA DE LA TORRETA GIRA HACIA EL JUGADOR
    //LA METRALLADORRA GIRA DE FORMA BRUSCA AL JUGADOR DIRECTO, LOS DEMAS NO
    private void FollowTarget() 
    {
        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;

        if (turretType == TurretType.Single)
        {
            turreyHead.forward = targetDir;
        }
        else
        {
            turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(targetDir), loockSpeed * Time.deltaTime);
        }
    }

    private void ShootTrigger()
    {
        Shoot(currentTarget);
    }
    
    //PROYECTIL (NO ESTA EN USO) CALCULATE-VELOCITY NO ESTA EN USO


    // DIBUJA EL RADIO DE ATAQUE
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }


    //ROTACION CUANDO EL JUGADOR NO ESTA EN EL RADIO DE LA TORRETA
    //LA VARIABLE REFRESHRANDOM NO ES NECESARIA
    public void IdleRotate()
    {
        if (turreyHead.rotation != Quaternion.Euler(randomRot))
        {
            turreyHead.rotation = Quaternion.RotateTowards(turreyHead.transform.rotation, Quaternion.Euler(randomRot), loockSpeed * Time.deltaTime * 0.2f);
        }
        else
        {
            int randomAngle = Random.Range(0, 359);
            randomRot = new Vector3(0, randomAngle, 0);
        }
    }


    //TIPOS DE DISPARO, DISPARAR
    public void Shoot(GameObject go)
    {
        if (turretType == TurretType.Catapult)
        {
            lockOnPos = go.transform;
            //Aplicar POOL OBJECT
            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
            Projectile projectile = missleGo.GetComponent<Projectile>();
            projectile.target = lockOnPos;
        }
        else if(turretType == TurretType.Dual)

        {
            if (shootLeft)
            {
                //Aplicar POOL OBJECT
                Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
                GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
            }
            else
            {
                //Aplicar POOL OBJECT
                Instantiate(muzzleEff, muzzleSub.transform.position, muzzleSub.rotation);
                GameObject missleGo = Instantiate(bullet, muzzleSub.transform.position, muzzleSub.rotation);
                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
            }

            shootLeft = !shootLeft;
        }
        else
        {
            //Aplicar POOL OBJECT
            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
            Projectile projectile = missleGo.GetComponent<Projectile>();
            projectile.target = currentTarget.transform;
        }
    }
}
