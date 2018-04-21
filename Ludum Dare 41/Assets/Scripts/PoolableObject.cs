using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public float lifeTime = 0;

    public virtual void Destroy()
    {
        gameObject.SetActive(false);
    }


    protected virtual void OnEnable()
    {
        if (lifeTime > 0)
        {
            Invoke("Destroy", lifeTime);
        }
    }

    protected virtual void OnDisable()
    {
        CancelInvoke();
    }
}
