using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PooledObjectType
{
    Bullet,
    Missile,
    Turret,
    TimerUI,
    Pickup
}

public class ObjectPooler : Singleton<ObjectPooler>
{
    [System.Serializable]
    public class Pool
    {
        public PooledObjectType type;

        public GameObject gameObjectToPool;
        public int poolSize;
        public bool canExpand;


        [HideInInspector]
        public List<GameObject> objectPool;
        [HideInInspector]
        public GameObject waitingPool;
        [HideInInspector]
        public string name;

    }

    public List<Pool> pools;

    private Dictionary<PooledObjectType, Pool> poolDictionary = new Dictionary<PooledObjectType, Pool>();

    protected override void Awake()
    {
        base.Awake();
        FillObjectPools();
    }

    private void FillObjectPools()
    {
        foreach (Pool p in pools)
        {
            p.name = p.type.ToString();
            p.waitingPool = new GameObject(p.name + " Object Pooler");
            p.objectPool = new List<GameObject>();

            for (int i = 0; i < p.poolSize; i++)
            {
                AddObjectToPool(p);
            }
            poolDictionary.Add(p.type, p);
        }
    }

    private GameObject AddObjectToPool(Pool p)
    {
        if (p.gameObjectToPool == null)
        {
            return null;
        }

        GameObject newGameObject = Instantiate(p.gameObjectToPool);
        newGameObject.gameObject.SetActive(false);
        newGameObject.transform.SetParent(p.waitingPool.transform);
        newGameObject.name = p.gameObjectToPool.name + p.objectPool.Count;
        p.objectPool.Add(newGameObject);
        return newGameObject;
    }

    public GameObject GetPooledGameObject(PooledObjectType _type)
    {
        Pool tempPool;
        if (!poolDictionary.TryGetValue(_type, out tempPool))
        {
            return null;
        }

        for (int i = 0; i < tempPool.objectPool.Count; i++)
        {
            if (!tempPool.objectPool[i].gameObject.activeInHierarchy)
            {
                return tempPool.objectPool[i];
            }
        }
        if (tempPool.canExpand)
        {
            return AddObjectToPool(tempPool);
        }

        return null;
    }
}
