using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public AudioClip bgm;

    float timeToStartGame = 3;
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > timeToStartGame)
        {
            SceneManager.LoadScene("Player Select");
        }	
	}
}
