using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : PersistentSingleton<GameManager>
{
    [System.Serializable]
    public class PlayerInfo
    {
        public PlayerNumber number;
        public Sprite sprite;
    }

    public GameObject playerPrefab;

    public int numberOfPlayers;

    public List<Player> players =  new List<Player>();
    public static List<PlayerInfo> playersToSpawn = new List<PlayerInfo>();
    public Transform[] SpawnPoints =  new Transform[4];

    protected override void Awake()
    {
        base.Awake();   
        GameEventHandler.OnPlayerDeath += OnPlayerDeath;
    }

    public void SpawnPlayers()
    {
        for (int i = 0; i < playersToSpawn.Count; i++)
        {
            GameObject player = Instantiate(playerPrefab);
            Player p = player.GetComponent<Player>();
            p.pNumber = playersToSpawn[i].number;
            player.name = "Player " + p.pNumber;

            SpawnPoints[(int)p.pNumber] = GameObject.Find("Player " + p.pNumber + " Start").transform;
            p.Initialize(SpawnPoints[(int)p.pNumber].position, playersToSpawn[i].sprite);
        }
    }

    private void OnPlayerDeath(Player player)
    {
        player.Initialize(SpawnPoints[(int)player.pNumber].position, player.carImage.sprite);
    }


    public void InitializePlayers(List<PlayerSelect> p)
    {
        foreach (PlayerSelect ps in p)
        {
            PlayerInfo pi = new PlayerInfo
            {
                number = ps.playerNumber,
                sprite = ps.sprites.GetComponent<Image>().sprite
            };


            playersToSpawn.Add(pi);
        }


    }

    private void OnLevelWasLoaded(int level)
    {
        
    }


    private void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;

        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(SpawnPoints[0].position, .25f);

        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(SpawnPoints[1].position, .25f);

        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(SpawnPoints[2].position, .25f);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(SpawnPoints[3].position, .25f);
    }
}
