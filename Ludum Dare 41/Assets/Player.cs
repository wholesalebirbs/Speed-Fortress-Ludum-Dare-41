using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float ACCELERATION;
	public float MAXSPEED;
	public float ROTATION;
	private float health = 100;
	public UnityEngine.Sprite carImage;
	public float playerId;
	public float pickupDragDistance;

	private float speed = 0;
	private Rigidbody2D rb;
	private GameObject pickup;
	private GameObject turret;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		var spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = carImage;
		turret = (GameObject)Resources.Load("Prefab/Turret", typeof(GameObject));
	}

	void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Mathf.Round(-Input.GetAxis ("Horizontal"));
		if (moveHorizontal != 0) speed += moveHorizontal * ACCELERATION;
		else {
			if (speed > 0 && speed < ACCELERATION) speed = 0;
			else if (speed < 0 && speed > -ACCELERATION) speed = 0;
			else if (speed < 0) speed += ACCELERATION;
			else speed -= ACCELERATION;
		}

		if (speed > MAXSPEED) speed = MAXSPEED;
		if (speed < -MAXSPEED) speed = -MAXSPEED;

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Mathf.Round(-Input.GetAxis ("Vertical"));
		rb.rotation += moveVertical * ROTATION;
	
		rb.velocity = new Vector2 (transform.up.x, transform.up.y).normalized * speed;

		if (pickup != null) {
			pickup.transform.position = transform.position + transform.up * pickupDragDistance;
			pickup.transform.rotation = Quaternion.Euler(0, 0, rb.rotation);
		}

		if (Input.GetButton("Fire1") && pickup != null) {
			
			Instantiate(turret, pickup.transform.position, Quaternion.identity);
			Destroy(pickup);
			pickup = null;
		}
    }

	void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "pickup") {
			if (pickup != null) return;
			pickup = other.gameObject;
		}
    }
}
