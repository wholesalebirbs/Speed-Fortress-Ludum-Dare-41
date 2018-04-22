using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public GameObject playerPrefab;

    public int numberOfPlayers;

    public List<Player> players =  new List<Player>();
    public List<PlayerSelect> playersToSpawn = new List<PlayerSelect>();
    public Transform[] SpawnPoints =  new Transform[4];

    private void Awake()
    {

        GameEventHandler.OnPlayerDeath += OnPlayerDeath;
    }

    public void SpawnPlayers()
    {
        for (int i = 0; i < playersToSpawn.Count; i++)
        {
            GameObject player = Instantiate(playerPrefab);
            Player p = player.GetComponent<Player>();
            p.pNumber = playersToSpawn[i].playerNumber;
            p.Initialize(SpawnPoints[(int)p.pNumber].position, playersToSpawn[i].sprites.GetComponent<Texture2D>());
        }
    }

    private void OnPlayerDeath(Player player)
    {
        player.Initialize(SpawnPoints[(int)player.pNumber].position, null);
    }


    public void InitializePlayers(List<PlayerSelect> p)
    {
        playersToSpawn = new List<PlayerSelect>(p);
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
