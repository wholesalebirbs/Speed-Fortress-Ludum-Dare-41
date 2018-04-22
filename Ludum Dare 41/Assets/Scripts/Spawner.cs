using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public PooledObjectType objectToSpawn;

    public float minSpawnTime;
    public float maxSpawnTime;

    public float currentSpawnTime;
    public int maxNumberOfSpawns;
    

    public List<GameObject> activeSpawns = new List<GameObject>();
    public Bounds[] spawnAreas;
    
    
	// Use this for initialization
	void Start ()
    {
        GameEventHandler.OnPickupDestroyed += OnPickupDestroyed;

        Invoke("Spawn", GetRandomSpawnTime());
	}


    private void Spawn()
    {
        if (activeSpawns.Count >= maxNumberOfSpawns)
        {
            currentSpawnTime = maxSpawnTime;

            Invoke("Spawn", currentSpawnTime);
            return;
        }

        //grab the next inactive pickup
        GameObject p = ObjectPooler.Instance.GetPooledGameObject(PooledObjectType.Pickup);

        //select a random index from the spawn areas
        int randomBoundIndex = Random.Range(0, spawnAreas.Length);

        //grab a position inside the spawn areas based on the random index
        Vector3 pos = new Vector3(
            Random.Range(spawnAreas[randomBoundIndex].center.x - spawnAreas[randomBoundIndex].extents.x, spawnAreas[randomBoundIndex].center.x + spawnAreas[randomBoundIndex].extents.x),
            Random.Range(spawnAreas[randomBoundIndex].center.y - spawnAreas[randomBoundIndex].extents.y, spawnAreas[randomBoundIndex].center.y + spawnAreas[randomBoundIndex].extents.y));

        activeSpawns.Add(p);
        p.GetComponent<Pickup>().Initialize(pos);

        Invoke("Spawn", GetRandomSpawnTime());
    }

    private float GetRandomSpawnTime()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void OnPickupDestroyed(Pickup pickup)
    {
        GameObject pU = pickup.gameObject;

        for (int i = 0; i < activeSpawns.Count; i++)
        {
            if (activeSpawns[i] == pU)
            {
                activeSpawns.RemoveAt(i);
                return;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = Color.cyan;

        if (spawnAreas.Length == 0)
        {
            return;
        }

        for (int i = 0; i < spawnAreas.Length; i++)
        {
            Gizmos.DrawLine(spawnAreas[i].center + new Vector3(spawnAreas[i].extents.x, spawnAreas[i].extents.y), spawnAreas[i].center + new Vector3(-spawnAreas[i].extents.x, spawnAreas[i].extents.y));
            Gizmos.DrawLine(spawnAreas[i].center + new Vector3(-spawnAreas[i].extents.x, -spawnAreas[i].extents.y), spawnAreas[i].center + new Vector3(spawnAreas[i].extents.x, -spawnAreas[i].extents.y));
            Gizmos.DrawLine(spawnAreas[i].center + new Vector3(spawnAreas[i].extents.x, spawnAreas[i].extents.y), spawnAreas[i].center + new Vector3(spawnAreas[i].extents.x, -spawnAreas[i].extents.y));
            Gizmos.DrawLine(spawnAreas[i].center + new Vector3(-spawnAreas[i].extents.x, spawnAreas[i].extents.y), spawnAreas[i].center + new Vector3(-spawnAreas[i].extents.x, -spawnAreas[i].extents.y));
        }
    }
}
