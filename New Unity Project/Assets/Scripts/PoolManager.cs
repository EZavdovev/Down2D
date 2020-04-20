using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static Dictionary<string, LinkedList<GameObject>> PoolsDictionary;
    private static Transform DeactivatedObjectsParent;
    
    public static void init(Transform pooledObjectsContainer)
    {
        DeactivatedObjectsParent = pooledObjectsContainer;
        PoolsDictionary = new Dictionary<string, LinkedList<GameObject>>();
    }

    public static GameObject getGameObjectFromPool(GameObject prefab, Transform pref)
    {
        if (!PoolsDictionary.ContainsKey(prefab.name))
        {
            PoolsDictionary[prefab.name] = new LinkedList<GameObject>();
        }
        GameObject result;
        if(PoolsDictionary[prefab.name].Count > 0)
        {
            result = PoolsDictionary[prefab.name].First.Value;
            PoolsDictionary[prefab.name].RemoveFirst();
            
            result.transform.position = pref.position;
            result.transform.rotation = pref.rotation;
            result.SetActive(true);
            
            return result;
        }
        result = Instantiate(prefab, pref.position, pref.rotation);
        result.name = prefab.name;

        return result;
    }

    public static void putGameObjectToPool(GameObject target)
    {
        PoolsDictionary[target.name].AddFirst(target);
        target.transform.position = DeactivatedObjectsParent.position;
        target.SetActive(false);
    }
   
}
