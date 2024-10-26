using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualProyectileMovement : ProjectileMovement
{
    protected override void Movement()
    {
        Vector3 dir = target.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.red);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
