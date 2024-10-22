using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DestroyProyectile : MonoBehaviour
{
    [Header("TARGET")]
    public Transform target;
    public float knockBack = 0.1f;

    [Header("EXPLOSION")]
    public float boomTimer = 1;
    public ParticleSystem explosion;



    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        ExplosionTimer();
    }



    private  void ExplosionTimer()
    {
        boomTimer -= Time.deltaTime;

        if (CanDestroyProyectile())
            Explosion();

    }

    private bool CanDestroyProyectile()
    {
        return target == null || transform.position.y < -0.2f || boomTimer < 0;
    }

    public void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }



    private void OnTriggerEnter(Collider other)
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


}
