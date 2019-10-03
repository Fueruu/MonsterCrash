using UnityEngine;

public class PooledObject : MonoBehaviour
{

    ObjectPool pooledInstance;

    // Template for object pool
    public T GetPooledInstance<T> () where T : PooledObject
    {
        if (!pooledInstance)
        {
            pooledInstance = ObjectPool.GetPool(this);
        }
        return (T)pooledInstance.GetObject();
    }

    public ObjectPool Pool { get; set; }

    public void ReturnToPool()
    {
        if (Pool)
        {
            Pool.AddObject(this);
        }
        else
        {
            Debug.Log("End me.");
            Destroy(gameObject);
        }
    }
}