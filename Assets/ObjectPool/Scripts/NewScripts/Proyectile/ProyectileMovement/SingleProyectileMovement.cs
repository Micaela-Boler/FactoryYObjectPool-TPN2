using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleProyectileMovement : ProjectileMovement
{
    private void Start()
    {
        Vector3 dir = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    protected override void Movement()
    {
        float singleSpeed = speed * Time.deltaTime;
        transform.Translate(transform.forward * singleSpeed * 2, Space.World);
    }
}
