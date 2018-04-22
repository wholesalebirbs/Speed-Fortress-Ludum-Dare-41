using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : Singleton<CharacterSelectionManager>
{
    [System.Serializable]
    public class PlayerSprites
    {
        public PlayerNumber playerNumber;
        public Sprite[] sprites;
    }

    public List<PlayerSprites> playerSprites = new List<PlayerSprites>();

    public List<PlayerSelect> playersInGame = new List<PlayerSelect>();

    public float timeToSwitchScene;

    public bool countingDown = false;
    float countDownTimer = 3;

    protected override void Awake()
    {
        base.Awake();
        GameEventHandler.OnPlayerEnterGame += OnPlayerEnterGame;
        GameEventHandler.OnPlayerLeaveGame += OnPlayerLeaveGame;
        GameEventHandler.OnPlayerReady += OnPlayerReady;
    }

    private void OnPlayerEnterGame(PlayerSelect playerSelect)
    {
        playersInGame.Add(playerSelect);
    }

    private void OnPlayerLeaveGame(PlayerSelect playerSelect)
    {
        for (int i = 0; i < playersInGame.Count; i++)
        {
            if (playersInGame[i] == playerSelect)
            {
                playersInGame.RemoveAt(i);
            }
        }
    }

    private void OnPlayerReady(PlayerSelect playerSelect)
    {
        if(playersInGame.Count < 2)
        {
            return;
        }

        for (int i = 0; i < playersInGame.Count; i++)
        {
            if (playersInGame[i].GetComponent<PlayerSelect>().state != PlayerSelect.SelectState.Ready)
            {
                return;
            }
        }

        countingDown = true;

        
    }

    private void Update()
    {
        if (countingDown)
        {
            countDownTimer -= Time.deltaTime;
            if (countDownTimer <= 0)
            {
                GameManager.Instance.InitializePlayers(playersInGame);
                SceneManager.LoadScene("GAME");
            }

            Debug.Log(countDownTimer);
        }
    }
}
