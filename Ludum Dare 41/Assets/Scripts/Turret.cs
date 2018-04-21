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
    public float shootingForce= .5f;
    public Transform firePoint;

    public int id;
	// Use this for initialization
	protected override void OnEnable ()
    {
        base.OnEnable();
        GetComponent<CircleCollider2D>().radius = range;
		
	}

   // void UpdateTarget()
   // {
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    //    float shortestDistance = Mathf.Infinity;
   //     GameObject nearestEnemy = null;
    //    foreach(GameObject enemy in enemies)
    //    {
      //      float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
      //      if(distanceToEnemy<shortestDistance)
     //       {
    //            shortestDistance = distanceToEnemy;
    //            nearestEnemy = enemy;
    //        }
   //     }
  //      if (nearestEnemy != null &&shortestDistance<=range)
  //      {
  //          target = nearestEnemy.transform;
   //     }
   //     else
   //     {
    //        target = null;
     //   }
  //  }
	
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
        GameObject bulletGo=ObjectPooler.Instance.GetPooledGameObject();
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        Rigidbody2D bulletRB = bulletGo.GetComponent<Rigidbody2D>();
        bulletRB.velocity= transform.TransformDirection(Vector3.forward * shootingForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if(p==null)
        {
            return;
        }

        if(p.playerId==id)
        {
            return;
        }

        target = p.gameObject.transform;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(target.gameObject==collision.gameObject)
        {
            target = null;
        }
        Physics2D.OverlapCircle(transform.position, range);
    }

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);


    }
}
