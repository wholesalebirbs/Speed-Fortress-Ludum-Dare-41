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

    public GameObject carSprite;

    public GameObject joinText;
    public GameObject readyText;

    public CharacterSelectionManager csm;

    public int spriteIndex = 0;

    float axisWaitTime = .3f;
    float axisWaitTimer = 0;

    bool axisInUse = false;
	// Use this for initialization
	void Start ()
    {
        titleText.text = "Player " + playerNumber;
        carSprite.SetActive(false);
        readyText.SetActive(false);
        SwitchSprite(spriteIndex);
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
                if (Input.GetAxisRaw(playerNumber + " Left X Axis") == 1)
                {
                    if (axisWaitTimer >= axisWaitTime)
                    {
                        axisWaitTimer = 0;
                        spriteIndex++;
                        SwitchSprite(spriteIndex);
                    }
                }
                else if(Input.GetAxisRaw(playerNumber + " Left X Axis") == -1)
                {
                    if (axisWaitTimer >= axisWaitTime)
                    {
                        axisWaitTimer = 0;
                        spriteIndex++;
                        SwitchSprite(spriteIndex);
                    }
                }

                axisWaitTimer += Time.deltaTime;
                

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
        carSprite.SetActive(true);
        state = SelectState.Selecting;
    }

    private void NotInGame()
    {
        carSprite.SetActive(false);
        joinText.SetActive(true);
        state = SelectState.NotInGame;
        GameEventHandler.CallPlayerLeaveGame(this);
    }

    private void Ready()
    {
        readyText.SetActive(true);
        carSprite.SetActive(false);
        state = SelectState.Ready;
        GameEventHandler.CallPlayerReady(this);
    }

    public void InGame()
    {
        GameEventHandler.CallPlayerEnterGame(this);
        axisWaitTimer = 1;
        joinText.SetActive(false);
        carSprite.SetActive(true);
        state = SelectState.Selecting;
    }


    private void SwitchSprite(int index)
    {
        if (index < 0)
        {
            index = 4;
            spriteIndex = index;
        }

        carSprite.GetComponent<Image>().sprite =  csm.GetPlayerSprite(playerNumber, index);
    }

}
