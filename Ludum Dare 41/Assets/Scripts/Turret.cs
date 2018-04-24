using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ShootingObject
{
    public List<ITarget> possibleTargets = new List<ITarget>();
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
        possibleTargets = new List<ITarget>();

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
        ITarget t = collision.GetComponent<ITarget>();

        //check if tower isn't null, tower will always get picked over other player
        if (t != null)
        {
            if (t.PlayerNUmber == _id)
            {
                return;
            }
            else
            {
                possibleTargets.Add(t);
                target = t.Position;
                return;
            }
        }

    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        ITarget t = collision.GetComponent<ITarget>();
        if (t == null)
        {
            return;
        }
        if (t.PlayerNUmber == _id)
        {
            return;
        }

        for (int i = 0; i < possibleTargets.Count; i++)
        {
            if (possibleTargets[i] == t)
            {
                possibleTargets.RemoveAt(i);
            }
        }

        if (possibleTargets.Count > 0)
        {
            target = possibleTargets[0].Position;
        }
    }
}
