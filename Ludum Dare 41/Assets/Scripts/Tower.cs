using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    private Transform target;
    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float range = 15f;
    [Header("Unity set up fields")]
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    public float shootingForce = .5f;
    public Transform firePoint;
    private float towerHealth;

    public int id;

    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = range;
    }

    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if (target == null)
            return;
        Vector3 dir = target.position - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation =Quaternion.Lerp(partToRotate.rotation, lookRotation,Time.deltaTime*turnspeed).eulerAngles;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

    }
    void Shoot()
    {
        GameObject MissileGo = ObjectPooler.Instance.GetPooledGameObject(PooledObjectType.Missile);
        Missile Missile = MissileGo.GetComponent<Missile>();
        Rigidbody2D bulletRB = MissileGo.GetComponent<Rigidbody2D>();
        bulletRB.velocity = transform.TransformDirection(Vector3.forward * shootingForce);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p == null)
        {
            return;
        }

        if ((int)p.pNumber == id)
        {
            return;
        }

        target = p.gameObject.transform;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target.gameObject == collision.gameObject)
        {
            target = null;
        }
        Physics2D.OverlapCircle(transform.position, range);
    }
    public void Initialize(Vector3 position, int _id)
    {
        transform.position = position;
        gameObject.SetActive(true);

        id = _id;
    }
}
