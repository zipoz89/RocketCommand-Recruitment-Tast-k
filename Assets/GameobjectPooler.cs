using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectPooler<T> where T : IPoolableObject
{
    private int maxPoolSize = 40;
    private int currentPoolSize = 0;
    public int CurrentPoolSize { get => CurrentPoolSize; }
    private GameObject objPrefab;

    public bool IsExpandable = true;

    private Queue<T> pool = new Queue<T>();
    public List<T> active = new List<T>();



    public void Initialize(GameObject prefab, int initialPoolSize = 20, bool isExpandable = true, int maxPoolSize = 40)
    {
        objPrefab = prefab;
        IsExpandable = isExpandable;
        this.maxPoolSize = maxPoolSize;

        for (int i = 0; i < initialPoolSize; i++)
        {
            Generate();
        }
    }

    private void Generate() 
    {
        T nuke = GameObject.Instantiate(objPrefab).GetComponent<T>();
        nuke.OnGenerated();
        pool.Enqueue(nuke);
        currentPoolSize++;
    }

    public void SetParams(int maxPoolSize) 
    {
        this.maxPoolSize = maxPoolSize;
    }

    // <summary>
    // takes one object from pool, generates new object if able, if not return null
    // </summary>
    public T Get() 
    {
        T obj = TryTake();

        if (obj == null && currentPoolSize < maxPoolSize && IsExpandable) 
        {
            Generate();
            currentPoolSize++;
            obj = TryTake();
        }

        if (obj != null)
        {
            obj.OnSpawned();
        }

        return obj;
    }

    // tries to take one object from pool
    private T TryTake() 
    {
        T result = default(T);

        if (pool.Count > 0)
        { 
            result = pool.Dequeue();
            active.Add(result);
        }

        return result;
    }

    // <summary>
    // returns refferenced object to the pool
    // </summary>
    public void Return(T obj) 
    {
        active.Remove(obj);
        obj.OnReturned();
        pool.Enqueue(obj);
    }


}
