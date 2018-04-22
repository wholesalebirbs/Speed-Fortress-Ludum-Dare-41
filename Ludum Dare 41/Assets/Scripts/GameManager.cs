using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public GameObject playerPrefab;

    public int numberOfPlayers;

    public List<Player> players =  new List<Player>();
    public Transform[] SpawnPoints =  new Transform[4];

    private void Awake()
    {
        GameEventHandler.OnPlayerDeath += OnPlayerDeath;
    }

    public void SpawnPlayers()
    {
        playerPrefab = Resources.Load("Prefab/Player") as GameObject;

        if (playerPrefab == null)
        {
            Debug.Log("Player Prefab is null!");
            return;
        }
    }

    private void OnPlayerDeath(Player player)
    {
        player.Initialize(SpawnPoints[(int)player.pNumber].position, null);
    }


    private void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(SpawnPoints[0].position, .25f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(SpawnPoints[1].position, .25f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(SpawnPoints[2].position, .25f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(SpawnPoints[3].position, .25f);
    }
}
