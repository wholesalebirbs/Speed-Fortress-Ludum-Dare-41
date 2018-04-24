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
        GameEventHandler.OnTowerDeath += OnTowerDeath;
    }

    public void SpawnPlayers()
    {
        players = new List<Player>();
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

            players.Add(p);

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
                sprite = ps.carSprite.GetComponent<Image>().sprite
            };


            playersToSpawn.Add(pi);
        }


    }

    private void OnTowerDeath(Tower t)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (t._id == players[i]._id)
            {
                players[i].gameObject.SetActive(false);
                players.RemoveAt(i);
            }
        }

        CheckWinCondition();

        
    }

    private void CheckWinCondition()
    {
        if (players.Count < 2)
        {

            EndGame();
        }
    }

    private void EndGame()
    {
        for (int i = 0; i < towersInGame.Count; i++)
        {
            if (towersInGame[i].GetComponent<Tower>().isAlive)
            {
                towersInGame[i].GetComponent<Tower>().DisableColliders();
            }
        }
        GameEventHandler.CallOnPlayerWin(players[0]);
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
