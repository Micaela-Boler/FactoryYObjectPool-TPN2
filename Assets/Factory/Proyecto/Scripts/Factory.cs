using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Props[] props;

    private Dictionary<string, Props> propByName;



    private void Awake()
    {
        propByName = new Dictionary<string, Props>();

        foreach (var prop in props)
        {
            propByName.Add(prop.propName, prop);
        }
    }

    public Props CreateProp(string propName)
    {
        if (propByName.TryGetValue(propName, out Props propPrefab))
        {
            Props propInstance = Instantiate(propPrefab, gameObject.transform.position, Quaternion.identity);
            propInstance.transform.parent = gameObject.transform;

            return propInstance;
        }
        else
        {
            Debug.LogWarning($"El objeto '{propName}' no existe en la base de datos de -PROPS-.");
            return null;
        }
    }
}
