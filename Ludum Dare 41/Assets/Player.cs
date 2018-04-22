using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNumber
{
    One,
    Two,
    Three,
    Four,
}


public class Player : MonoBehaviour {

    private bool playerOffRoad;
    public float ACCELERATION;
	public float MAXSPEED;
	public float ROTATION;
    public float originalSpeed;

    [SerializeField]
    public float totalHealth = 100;
	public float currentHealth;

    public UnityEngine.Sprite carImage;
	public PlayerNumber pNumber;

	public float pickupDragDistance;

    //private bool isHitBullet;
    //private bool isHitMissile;


    [SerializeField]
    private bool playerOffMap;

	private float speed = 0;
    private float offTrackSpeed;
	private Rigidbody2D rb;
	private GameObject pickup;
	private GameObject turret;

	// Use this for initialization
	void Start ()
    {
        currentHealth = totalHealth;
		rb = GetComponent<Rigidbody2D>();
		var spriteRenderer = GetComponent<SpriteRenderer>();
		//spriteRenderer.sprite = carImage;
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

            pickup.GetComponent<Collider2D>().enabled = false;
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
        float vertical = Mathf.Round(-Input.GetAxis (pNumber.ToString() + " Left Y Axis"));
		if (vertical != 0) speed += vertical * ACCELERATION;
		else {
			if (speed > 0 && speed < ACCELERATION) speed = 0;
			else if (speed < 0 && speed > -ACCELERATION) speed = 0;
			else if (speed < 0) speed += ACCELERATION;
			else speed -= ACCELERATION;
		}

		if (speed > MAXSPEED) speed = MAXSPEED;
		if (speed < -MAXSPEED) speed = -MAXSPEED;

        //Store the current vertical input in the float moveVertical.
        float horizontal = Mathf.Round(-Input.GetAxis (pNumber.ToString() + " Right X Axis"));
        //Debug.Log(horizontal * ROTATION);
		rb.rotation += horizontal * ROTATION;

		rb.velocity = new Vector2 (transform.up.x, transform.up.y).normalized * speed;

		if (pickup != null)
        {
			pickup.transform.position = transform.position + transform.up * pickupDragDistance;
			pickup.transform.rotation = Quaternion.Euler(0, 0, rb.rotation);
		}

		if (Input.GetAxis(pNumber + " Right Trigger") > 0.5 && pickup != null)
        {
            Debug.Log("Player " + pNumber + "Spawned a turret");
            GameObject t = ObjectPooler.Instance.GetPooledGameObject(PooledObjectType.Turret);
            if (t != null)
            {
                t.GetComponent<Turret>().Initialize(pickup.transform.position, pNumber);
                //Instantiate(turret, pickup.transform.position, Quaternion.identity);
                //Destroy(pickup);



                if (pickup != null)
                {
                    pickup.GetComponent<Pickup>().Destroy();
                    pickup = null;
                }
                
            }
            else
            {
                Debug.Log("Something went wrong, Player " + pNumber.ToString() + "could not spawn turret");
            }

		}
        if (playerOffMap == true)
        {
            //rb.velocity = new Vector2(0, 0);
            //speed = speed / -2;
            //rb.angularVelocity = 0;
            playerOffMap = false;
        }
        else rb.velocity = new Vector2(transform.up.x, transform.up.y).normalized * (playerOffRoad == true ? speed * 0.5f : speed);

        //if (isHitBullet == true)
        //{
        //    currentHealth = currentHealth - 10;
        //    isHitBullet = false;
        //}
        //if (isHitMissile == true)
        //{
        //    currentHealth = currentHealth - 25;
        //    isHitMissile = false;
        //}
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {

        GameEventHandler.CallOnPlayerDeath(this);
    }


    public void Initialize(Vector3 position, Sprite vehicleSprite)
    {
        currentHealth = totalHealth;
        transform.position = position;
    }
}

