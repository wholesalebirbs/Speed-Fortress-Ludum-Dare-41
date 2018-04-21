using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        GameManager.Instance.numberOfPlayers = 4;
        GameManager.Instance.SpawnPlayers();
	}

}
