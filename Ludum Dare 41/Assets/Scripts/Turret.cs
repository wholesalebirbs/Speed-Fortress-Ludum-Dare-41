using System.Collections;
using UnityEngine;

public class Turret : PoolableObject {
    private Transform target;
    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float range = 15f;
    [Header("Unity set up fields")]
    public string enemyTag = "Enemy";

    public float turnspeed = 10;
    public Transform partToRotate;

    public GameObject bulletPrefab;
    public float shootingForce= 100f;
    public Transform firePoint;

    PlayerNumber id;

    AudioClip shootingSFX;


    private void Start()
    {
        shootingSFX = Resources.Load("Audio/turret shooting") as AudioClip;
    }

    // Use this for initialization
    protected override void OnEnable ()
    {
        base.OnEnable();
        GetComponent<CircleCollider2D>().radius = range;
		
	}

	
	// Update is called once per frame
	void Update () {
        if (target == null)
            return;
        Vector3 dir = target.position - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float rotationOffset = 0;
        //Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation =Quaternion.Lerp(partToRotate.rotation, lookRotation,Time.deltaTime*turnspeed).eulerAngles;
        partToRotate.rotation = Quaternion.RotateTowards(partToRotate.rotation, Quaternion.Euler(0f, 0f,angle+rotationOffset),200*Time.deltaTime);
		
        if(fireCountdown<=0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
	}
    void Shoot()
    {
        GameObject bulletGo = ObjectPooler.Instance.GetPooledGameObject(PooledObjectType.Bullet);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        bullet.Initialize(firePoint.position, partToRotate.rotation, id);
        Rigidbody2D bulletRB = bulletGo.GetComponent<Rigidbody2D>();
        bulletRB.velocity = partToRotate.TransformDirection(transform.right * shootingForce);
        //bulletGo.transform.forward = transform.TransformDirection(transform.right * shootingForce);
        //bulletRB.AddForce(transform.TransformDirection(transform.forward * shootingForce));

        AudioManager.Instance.PlaySound(shootingSFX, transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckForTarget(collision);
    }

    private void CheckForTarget(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p == null)
        {
            return;
        }

        if (p.pNumber == id)
        {
            return;
        }

        target = p.gameObject.transform;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D called on" + gameObject.name);

        if (target != null)
        {
            if (collision.gameObject == target.gameObject)
            {
                target = null;
            }
        }


        //if (Physics2D.OverlapCircle(transform.position, range) != null)
        //{
        //    CheckForTarget(collision);
        //}
        
    }

    public void Initialize(Vector3 position, PlayerNumber _id)
    {
        transform.position = position;
        id = _id;
        gameObject.SetActive(true);

        id = _id;

        UIEventHandler.CallTurretSpawned(this);
    }
}
