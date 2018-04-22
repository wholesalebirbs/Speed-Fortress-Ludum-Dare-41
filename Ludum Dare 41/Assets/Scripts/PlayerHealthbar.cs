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
        
 	}
	
	// Update is called once per frame
	void Update ()
    {
        if (target == null)
        {
            target = GameObject.Find("Player " + number.ToString());
        }
        if (target == null)
        {
            return;
        }
        player = target.GetComponent<Player>();

        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        transform.position = screenPos;

        bar.fillAmount = player.currentHealth / player.totalHealth;
    }
}
