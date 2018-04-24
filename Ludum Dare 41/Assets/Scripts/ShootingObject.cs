using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingObject : PoolableObject {

    public PlayerNumber _id;

    [Header("Attributes")]
    public float fireRate = 1f;
    public float fireForce = 5f;
    public float range = 15f;
    protected float fireCountdown = 0f;
    public float turnSpeed = 10;

    [Header("GameObjects")]
    public Transform target;
    public Transform partToRotate;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public AudioClip shootingSFX;

    public virtual void Initialize(Vector3 position, PlayerNumber id)
    {
        transform.position = position;
        _id = id;
        gameObject.SetActive(true);
    }

	// Update is called once per frame
	protected virtual void Update ()
    {
        if (target == null)
            return;
        Vector3 dir = target.position - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float rotationOffset = 0;

        partToRotate.rotation = Quaternion.RotateTowards(partToRotate.rotation, Quaternion.Euler(0f, 0f, angle + rotationOffset), 200 * Time.deltaTime);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }


    protected virtual void Shoot()
    {
        //pew pew
    }

    protected virtual void CheckForTarget(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p == null)
        {
            return;
        }

        if (p._id == _id)
        {
            return;
        }

        target = p.gameObject.transform;

    }


    //triggers
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        CheckForTarget(collision);
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (target != null)
        {
            if (collision.gameObject == target.gameObject)
            {
                target = null;
            }
        }

    }


    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius);
    }


}
