using UnityEngine;

public class Bullet : PoolableObject
{

    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
