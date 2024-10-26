using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DestroyProyectile : MonoBehaviour
{
    [Header("TARGET")]
    public float knockBack = 0.1f;

    [Header("EXPLOSION")]
    public float boomTimer = 1;
    public ParticleSystem explosion;



    protected void Update()
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
        return transform.position.y < -0.2f || boomTimer < 0;
    }

    protected virtual void Explosion()
    {
        Debug.Log("DESTRUIR");
        InstantiateExplosion();
        Destroy(gameObject);
    }

    protected void InstantiateExplosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }



    protected void OnTriggerEnter(Collider other)
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
