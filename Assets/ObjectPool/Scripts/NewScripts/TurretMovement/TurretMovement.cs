using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [Header("ROTATION")]
    public Transform turreyHead;
    private Vector3 randomRot;
    public float loockSpeed;
    protected Vector3 targetDir;

    [Header("OTHER SCRIPTS")]
    public FindTarget findTarget;



    private void Start()
    {
        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }

    protected void Update()
    {
        FollowOrRotate();
    }



     void FollowOrRotate()
     {
        if (findTarget.currentTarget != null)
        {
            FollowTarget();

            float currentTargetDist = Vector3.Distance(transform.position, findTarget.currentTarget.transform.position);

            if (currentTargetDist > findTarget.attackDist) 
            {
                findTarget.currentTarget = null;
            }
        }
        else
        {
            IdleRotate();
        }
     }


    protected virtual void FollowTarget()
    {
        TargetDirection();
        turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(targetDir), loockSpeed * Time.deltaTime);
    }

    protected void TargetDirection()
    {
        targetDir = findTarget.currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;
    }


    private void IdleRotate()
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
}
