using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileMovement : MonoBehaviour
{
    public float speed = 1;
    public float turnSpeed = 1;
    public Transform target; 


    protected void Update()
    {
        Movement();
    }

    protected abstract void Movement();
}
