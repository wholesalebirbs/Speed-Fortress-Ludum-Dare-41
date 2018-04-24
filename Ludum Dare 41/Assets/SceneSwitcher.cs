using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    float timeToSwitchScenes = 3;
    float timer = 0;
	// Use this for initialization
	void Start ()
    {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= timeToSwitchScenes)
        {
            SceneManager.LoadScene("Player Select");
        }
	}
}
