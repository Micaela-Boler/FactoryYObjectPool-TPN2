using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TurretAI;

public class PrepareShot : MonoBehaviour
{
    [HideInInspector] public GameObject currentTarget;
    public float attackDist = 10.0f;
    public float shootCoolDown;
    private float timer;

    [Header("ANIMATOR")]
    public Animator animator;

    [Header("OTHER SCRIPTS")]
    public ShotType shotType;



    protected void Start()
    {
        InvokeRepeating("CheckForTarget", 0, 0.5f);


        if (transform.GetChild(0).GetComponent<Animator>())
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }
    }

    private void Update()
    {
        ShotCooldown();
    }

    private void ShotCooldown()
    {
        timer += Time.deltaTime;

        if (timer >= shootCoolDown)
        {
            if (currentTarget != null)
            {
                timer = 0;

                if (animator != null)
                {
                    animator.SetTrigger("Fire");
                    shotType.Shoot();
                }
                else
                {
                    shotType.Shoot();
                }
            }
        }
    }


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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }
}
