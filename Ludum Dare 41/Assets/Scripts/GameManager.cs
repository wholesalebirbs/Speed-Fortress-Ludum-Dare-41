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
    public List<GameObject> towersInGame = new List<GameObject>();
    public Transform[] SpawnPoints =  new Transform[4];

    protected override void Awake()
    {
        base.Awake();   
        GameEventHandler.OnPlayerDeath += OnPlayerDeath;
    }

    public void SpawnPlayers()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            PlayerNumber spawnIndex = (PlayerNumber) i;
            SpawnPoints[i] = GameObject.Find("Player " + spawnIndex + " Start").transform;
        }

        TowerManager tm = FindObjectOfType<TowerManager>();

        for (int i = 0; i < playersToSpawn.Count; i++)
        {
            GameObject playerGO = Instantiate(playerPrefab);
            Player p = playerGO.GetComponent<Player>();
            p._id = playersToSpawn[i].number;
            playerGO.name = "Player " + p._id;

            GameObject tempTower = tm.towers[(int)p._id].gameObject;
            tempTower.SetActive(true);
            towersInGame.Add(tempTower);

            p.Initialize(SpawnPoints[(int)p._id].position, playersToSpawn[i].sprite);
        }
    }

    private void OnPlayerDeath(Player player)
    {
        player.Initialize(SpawnPoints[(int)player._id].position, player.carImage.sprite);
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
