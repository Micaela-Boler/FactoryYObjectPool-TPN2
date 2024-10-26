using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CatapultProyectileMovement : ProjectileMovement
{
    private bool catapult;
    private bool lockOn;
    

    void Start()
    {
        if (catapult) 
        {
            lockOn = true;
        }
    }


    protected override void Movement()
    {
        if (lockOn)
        {
            Vector3 Vo = CalculateCatapult(target.transform.position, transform.position, 1);

            transform.GetComponent<Rigidbody>().velocity = Vo;
            lockOn = false;
        }
    }

    Vector3 CalculateCatapult(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }
}
