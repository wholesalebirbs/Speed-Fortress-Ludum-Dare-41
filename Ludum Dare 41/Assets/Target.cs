using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : PoolableObject
{
    public void Initialize(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }
}
