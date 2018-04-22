using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStart : MonoBehaviour {
    public GameObject countDown;
	// Use this for initialization
	void Start () {
        StartCoroutine("startDelay");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator startDelay()
    {
        Time.timeScale = 0;
        float pauseTime=Time.realtimeSinceStartup + 8.2f;
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        countDown.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
