using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    public Vector3 _target;
    public float _seekSpeed;
    public float minDestroyDistance = 0.05f;

    private GameObject targetGraphic;

    public void Initialize(Vector3 position, Quaternion rotation, PlayerNumber _id, Vector3 target)
    {
        transform.position = position;
        transform.rotation = rotation;
        _target = target;

        targetGraphic = ObjectPooler.Instance.GetPooledGameObject(PooledObjectType.Target);
        targetGraphic.GetComponent<Target>().Initialize(_target);

        base._id = _id;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_target != Vector3.zero)
        {
            //transform.LookAt(_target);
            Vector3.MoveTowards(transform.position, _target, _seekSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _target) <= minDestroyDistance)
            {
                Destroy();
            }
        }
    }

    public override void Destroy()
    {

        _target = Vector3.zero;
        targetGraphic.GetComponent<Target>().Destroy();
        targetGraphic = null;
        base.Destroy();
    }

}
