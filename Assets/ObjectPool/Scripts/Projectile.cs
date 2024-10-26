﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    //SEPARAR EN BASE A LAS CLASES

    public TurretAI.TurretType type = TurretAI.TurretType.Single;
    public Transform target; // --
    public bool lockOn;

    public float speed = 1;
    public float turnSpeed = 1;
    public bool catapult;

    public float knockBack = 0.1f; //
    public float boomTimer = 1; //

    public ParticleSystem explosion; //

    private void Start()
    {
        if (catapult) //CATAPULTA------------
        {
            lockOn = true;
        }

        if (type == TurretAI.TurretType.Single) // SINGLE
        {
            Vector3 dir = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    private void Update() //TODOS------------
    {
        if (target == null) // SI NO HAY JUGADOR SE DESTRUYE EL PROYECTIL
        {
            Explosion();
            return;
        }

        if (transform.position.y < -0.2F)
        {
            Explosion();
        }

        boomTimer -= Time.deltaTime; //UTIL
        if (boomTimer < 0)
        {
            Explosion();
        }


        //PROYECTILE MOVEMENT

        if (type == TurretAI.TurretType.Catapult) //CATAPULTA--------------------
        {
            if (lockOn)
            {
                Vector3 Vo = CalculateCatapult(target.transform.position, transform.position, 1);

                transform.GetComponent<Rigidbody>().velocity = Vo;
                lockOn = false;
            }
        }else if(type == TurretAI.TurretType.Dual) //DUAL------------

        {
            Vector3 dir = target.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);


            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.LookRotation(newDirection);

        }else if (type == TurretAI.TurretType.Single) //SINGLE------------
        {
            float singleSpeed = speed * Time.deltaTime;
            transform.Translate(transform.forward * singleSpeed * 2, Space.World);
        }
    }

    Vector3 CalculateCatapult(Vector3 target, Vector3 origen, float time) //CATAPULTA----------------
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




    //SI COLISIONA CON EL JUGADOR
    private void OnTriggerEnter(Collider other) //TODOS--------------------
    {
        if (other.transform.tag == "Player")
        {
            Vector3 dir = other.transform.position - transform.position;
            Vector3 knockBackPos = other.transform.position + (dir.normalized * knockBack);
            knockBackPos.y = 1;
            other.transform.position = knockBackPos;

            Explosion();
        }
    }

    //INSTANCIA LA EXPLOSION Y DESTRUYE EL PROYECTIL // REEMPLAZAR COR POOL OBJETC-------------------------
    public void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
