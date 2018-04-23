using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : PoolableObject
{
    public override void Destroy()
    {
        GameEventHandler.CallOnPickupDestroyed(this);
        base.Destroy();
    }

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(true);
    }
}
