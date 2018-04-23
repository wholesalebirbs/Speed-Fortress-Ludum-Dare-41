using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PoolableObject
{
    [SerializeField]
    protected PlayerNumber _id;

    [SerializeField]
    protected int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        IHealthInterface h = collision.gameObject.GetComponent<IHealthInterface>();
        if (h == null)
        {
            //instantiate explosion
            Destroy();
            return;
        }

        h.TakeDamage(damage, _id);

        //Effects etc
        Destroy();
    }
}
