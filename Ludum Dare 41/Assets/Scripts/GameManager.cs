﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    public int numberOfPlayers;


    public List<Player> players;

    public Transform spawnLocation;

    void SpawnPlayers()
    {

        if (spawnLocation == null)
        {
            Debug.Log("Spawn Location is null");
            return;
        }

        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject go = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
            players.Add(go.GetComponent<Player>());
        }

    }
}