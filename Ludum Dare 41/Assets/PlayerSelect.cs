using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{

    public enum SelectState
    {
        NotInGame,
        Selecting,
        Ready,
    }

    public PlayerNumber playerNumber;
    public Text titleText;

    public SelectState state = SelectState.NotInGame;

    public GameObject sprites;

    public GameObject joinText;
    public GameObject readyText;



	// Use this for initialization
	void Start ()
    {
        titleText.text = "Player " + playerNumber;
        sprites.SetActive(false);
        readyText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown(playerNumber + " A Button"))
        {
            Debug.Log(playerNumber + " A Button was pressed");
        }

        switch (state)
        {
            case SelectState.NotInGame:
                if (Input.GetButtonDown(playerNumber + " A Button"))
                {
                    InGame();
                }
                break;
            case SelectState.Selecting:
                if (Input.GetButtonDown(playerNumber + " A Button"))
                {
                    Ready();
                }
                if (Input.GetButtonDown(playerNumber + " B Button"))
                {
                    NotInGame();
                }
                break;
            case SelectState.Ready:
                if (Input.GetButtonDown(playerNumber + " B Button"))
                {
                    UnReady();
                }
                break;
            default:
                break;
        }
    }

    private void UnReady()
    {
        readyText.SetActive(false);
        state = SelectState.Selecting;
    }

    private void NotInGame()
    {
        sprites.SetActive(false);
        joinText.SetActive(true);
        state = SelectState.NotInGame;
        GameEventHandler.CallPlayerLeaveGame(this);
    }

    private void Ready()
    {
        readyText.SetActive(true);
        GameEventHandler.CallPlayerReady(this);
        state = SelectState.Ready;
    }

    public void InGame()
    {
        GameEventHandler.CallPlayerEnterGame(this);
        joinText.SetActive(false);
        sprites.SetActive(true);
        state = SelectState.Selecting;
    }



}
