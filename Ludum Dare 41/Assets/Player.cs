using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    private bool playerOffRoad;
    public float ACCELERATION;
	public float MAXSPEED;
	public float ROTATION;
    public float originalSpeed;
	private float playerHealth = 100;
	public UnityEngine.Sprite carImage;
	public float playerId;

	public float pickupDragDistance;

    private bool isHitBullet;
    private bool isHitMissile;
    private bool playerOffMap;


	private float speed = 0;
    private float offTrackSpeed;
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

    //Checks to see if the player is on the road
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "offRoad")
        {
            playerOffRoad = true;
        }
        if (col.tag == "pickup")
        {
            if (pickup != null) return;
            pickup = col.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "offRoad")
        {
            playerOffRoad = false;
        }
    }
    //checks to see if the player is in the playable area
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "offMap")
        {
           playerOffMap = true;
        }
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
        Debug.Log(moveVertical * ROTATION);
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
        if (playerOffMap == true)
        {
            rb.velocity = new Vector2(0, 0);
            speed = speed / -2;
            rb.angularVelocity = 0;
            playerOffMap = false;
        }
        else rb.velocity = new Vector2(transform.up.x, transform.up.y).normalized * (playerOffRoad == true ? speed * 0.5f : speed);

        if (isHitBullet == true)
        {
            playerHealth = playerHealth - 10;
            isHitBullet = false;
        }
        if (isHitMissile == true)
        {
            playerHealth = playerHealth - 25;
            isHitMissile = false;
        }
    }
}

