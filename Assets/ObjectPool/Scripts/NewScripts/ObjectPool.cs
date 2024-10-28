using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance { get; private set; }


    [Header("POOL OBJECT")]

    [SerializeField] private List<GameObject> pooledObstacles;

    [SerializeField] int amountToPool;

    [SerializeField] GameObject proyectile;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        AddProyectileToPool(amountToPool);
    }



    private void AddProyectileToPool(int quantityToAdd)
    {
        for (int i = 0; i < quantityToAdd; i++)
        {
            GameObject obstacle = Instantiate(proyectile);
            obstacle.SetActive(false);
            pooledObstacles.Add(obstacle);

            obstacle.transform.parent = transform;
        }
    }

    public GameObject GetProyectile()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObstacles[i].activeSelf)
            {
                if (pooledObstacles[i] != null)
                {
                    pooledObstacles[i].SetActive(true);
                    return pooledObstacles[i];
                }
            }
        }
        AddProyectileToPool(1);
        pooledObstacles[pooledObstacles.Count - 1].SetActive(true);
        return pooledObstacles[pooledObstacles.Count - 1];
    }
}
