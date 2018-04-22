using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public GameObject playerPrefab;

    public int numberOfPlayers;

    public List<Player> players =  new List<Player>();

    public void SpawnPlayers()
    {
        playerPrefab = Resources.Load("Prefab/Player") as GameObject;

        if (playerPrefab == null)
        {
            Debug.Log("Player Prefab is null!");
            return;
        }

    }
}
