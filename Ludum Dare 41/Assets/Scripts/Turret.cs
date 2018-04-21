using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {
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
    public float shootingForce=15;
    public Transform firePoint;
    
	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
		
	}

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy<shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null &&shortestDistance<=range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null)
            return;
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation =Quaternion.Lerp(partToRotate.rotation, lookRotation,Time.deltaTime*turnspeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.z, 0f);
		
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
        Rigidbody bulletRB = bulletGo.GetComponent<Rigidbody>();
        bulletRB.velocity= transform.TransformDirection(Vector3.forward * shootingForce);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
