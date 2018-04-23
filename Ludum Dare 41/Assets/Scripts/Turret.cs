using System.Collections;
using UnityEngine;

public class Turret : ShootingObject
{
    // Use this for initialization
    protected override void OnEnable ()
    {
        base.OnEnable();
        GetComponent<CircleCollider2D>().radius = range;
		
	}
    public override void Destroy()
    {
        target = null;
        base.Destroy();
    }

    public override void Initialize(Vector3 position, PlayerNumber _id)
    {
        base.Initialize(position, _id);

        UIEventHandler.CallTurretSpawned(this);
    }


    protected override void Shoot()
    {
        GameObject bulletGo = ObjectPooler.Instance.GetPooledGameObject(PooledObjectType.Bullet);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        bullet.Initialize(firePoint.position, partToRotate.rotation, _id);
        Rigidbody2D bulletRB = bulletGo.GetComponent<Rigidbody2D>();
        bulletRB.velocity = partToRotate.TransformDirection(transform.right * fireForce);
        
        AudioManager.Instance.PlaySound(shootingSFX, transform.position);
    }

    protected override void CheckForTarget(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        Tower t = collision.GetComponent<Tower>();

        //check if they're both null

        if (p == null && t == null)
        {
            return;
        }

        //check if tower isn't null, tower will always get picked over other player
        if (t != null)
        {
            if (t._id == _id)
            {
                return;
            }
            else
            {
                target = t.transform;
                return;
            }
        }

        if (p!= null)
        {
            if (p._id == _id)
            {
                return;
            }
            else
            {
                target = p.transform;
                return;
            }
        }
    }
}
