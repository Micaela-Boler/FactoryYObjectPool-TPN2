using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySingleProyectile : DestroyProyectile
{
    protected override void Explosion()
    {
        InstantiateExplosion();
        gameObject.SetActive(false);

        Debug.Log("Explosion");
    }
}
