using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    PooledObject prefab;

    List<PooledObject> availableObjects = new List<PooledObject>();

    public static ObjectPool GetPool(PooledObject prefab)
    {
        GameObject obj;
        ObjectPool pool;
        //  This is for debugging purposes in the editor
        if (Application.isEditor)
        {
            obj = GameObject.Find(prefab.name + "_Pool");
            if (obj)
            {
                pool = obj.GetComponent<ObjectPool>();
                if (pool)
                {
                    return pool;
                }
            }
        }
        // Put same objects together
        obj = new GameObject(prefab.name + "_Pool");
        pool = obj.AddComponent<ObjectPool>();
        pool.prefab = prefab;
        return pool;
    }

    public PooledObject GetObject()
    {
        PooledObject pooledObj;
        int lastAvailableIndex = availableObjects.Count - 1;
        if (lastAvailableIndex >= 0)
        {
            // Remove the object from the pool and activate it
            pooledObj = availableObjects[lastAvailableIndex];
            availableObjects.RemoveAt(lastAvailableIndex);
            pooledObj.gameObject.SetActive(true);
        }
        else
        {
            // If there is nothing available in our pool create something
            pooledObj = Instantiate<PooledObject>(prefab);
            pooledObj.transform.SetParent(transform, false);
            pooledObj.Pool = this;
        }
        return pooledObj;
    }

    public void AddObject(PooledObject pooledObj)
    {
        // Deactivate our item and add it back to the pool
        pooledObj.gameObject.SetActive(false);
        availableObjects.Add(pooledObj);
    }

}