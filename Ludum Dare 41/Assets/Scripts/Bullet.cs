﻿using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
            

    }
    public void Seek(Transform _target)
    {
        target = _target;
    }
	// Update is called once per frame
	void Update () {
		
	}
}