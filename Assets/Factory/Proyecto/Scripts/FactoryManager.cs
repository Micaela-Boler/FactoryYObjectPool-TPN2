using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    [SerializeField] Transform factoryObject;
    [SerializeField] int propAmount;

    public Factory factory;



    public void ReplaceProp(string propName)
    {
        DestroyLastProp();
        factory.CreateProp(propName);

    }

    public void DestroyLastProp()
    {
        propAmount = factoryObject.transform.childCount;

        if (propAmount >= 1)
        {
            for (int i = 0; i < propAmount; i++)
            {
                GameObject prop = factoryObject.transform.GetChild(i).gameObject;
                Destroy(prop);
            }
        }
    }
}
