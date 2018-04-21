using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    public GameObject GameObjectToPool;
    public int poolSize = 20;
    public bool poolCanExpand = true;

    private GameObject waitingPool;

    private List<GameObject> pooledGameObjects;

    protected override void Awake()
    {
        base.Awake();
        FillObjectPool();
    }

    private void FillObjectPool()
    {
        waitingPool = new GameObject("Object Pooler" + this.name);
        pooledGameObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            AddObjectToPool();
        }
    }

    private GameObject AddObjectToPool()
    {
        if (GameObjectToPool == null)
        {
            return null;
        }

        GameObject newGameObject = Instantiate(GameObjectToPool);
        newGameObject.gameObject.SetActive(false);
        newGameObject.transform.SetParent(waitingPool.transform);
        newGameObject.name = GameObjectToPool.name + pooledGameObjects.Count;
        pooledGameObjects.Add(newGameObject);
        return newGameObject;
    }


    public GameObject GetPooledGameObject()
    {
        for (int i = 0; i < pooledGameObjects.Count; i++)
        {
            if (!pooledGameObjects[i].gameObject.activeInHierarchy)
            {
                return pooledGameObjects[i];
            }
        }
        if (poolCanExpand)
        {
            return AddObjectToPool();
        }

        return null;
    }
}
