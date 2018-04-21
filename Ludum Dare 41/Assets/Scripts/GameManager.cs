using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public GameObject playerPrefab;

    public int numberOfPlayers;

    public List<Player> players =  new List<Player>();

    public Transform spawnLocation;

    public void SpawnPlayers()
    {
        spawnLocation = GameObject.Find("Player Spawn").transform;

        if (spawnLocation == null)
        {
            Debug.Log("Spawn Location is null");
            return;
        }

        playerPrefab = Resources.Load("Prefab/Player") as GameObject;

        if (playerPrefab == null)
        {
            Debug.Log("Player Prefab is null!");
            return;
        }


        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject go = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
            players.Add(go.GetComponent<Player>());
        }

    }
}
