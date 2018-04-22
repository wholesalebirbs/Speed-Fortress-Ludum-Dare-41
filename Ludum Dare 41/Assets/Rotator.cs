using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    float rotationSpeed = 50;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);	
	}
}
