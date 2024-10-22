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
        AddProyectileToPool();
    }



    private void AddProyectileToPool()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obstacle = Instantiate(proyectile);
            obstacle.SetActive(false);
            pooledObstacles.Add(obstacle);

            obstacle.transform.parent = transform;
        }
    }

    public GameObject GetObstacle()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObstacles[i].activeSelf)
            {
                return pooledObstacles[i];
            }
        }
        return null;
    }
}
