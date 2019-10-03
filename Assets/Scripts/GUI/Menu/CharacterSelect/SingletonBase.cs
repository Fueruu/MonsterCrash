using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    static T instance;

    bool persist = false;

    public static T Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    return null;
                }
                instance.Initialize();
            }
            return instance;
        }
    }

    #region Basic getters/setters
    public bool Persist
    {
        get { return persist; }
        protected set { persist = value; }
    }
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            instance.Initialize();
            if (persist)
                DontDestroyOnLoad(gameObject);
        }
    }

    virtual protected void Initialize() { }

    void OnApplicationQuit()
    {
        instance = null;
    }
}

