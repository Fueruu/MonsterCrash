using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DDOLManager
{
    public static List<GameObject> _DDOLObjects = new List<GameObject>();

    public static void DontDestroyOnLoad(GameObject ddoObject)
    {
        UnityEngine.Object.DontDestroyOnLoad(ddoObject);
        _DDOLObjects.Add(ddoObject);
    }

    public static void DestroyAll()
    {
        foreach (var instance in _DDOLObjects)
        {
            if (instance != null)
            {
                UnityEngine.Object.Destroy(instance);
                //Debug.LogFormat("Destroyed: {0}", instance);
            }
        }
        _DDOLObjects.Clear();
    }
}
