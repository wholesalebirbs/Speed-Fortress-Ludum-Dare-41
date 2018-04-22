using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    GameObject target;
    public Image bar;


    Player player;

    public PlayerNumber number;
	// Use this for initialization
	void Start ()
    {
        target = GameObject.Find("Player " + number.ToString());
        player = target.GetComponent<Player>();
 	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        transform.position = screenPos;

        bar.fillAmount = player.currentHealth / player.totalHealth;
    }
}
