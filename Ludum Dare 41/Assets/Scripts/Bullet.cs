using UnityEngine;

public class Bullet : Projectile
{

    public void Initialize(Vector3 position, Quaternion rotation, PlayerNumber _id)
    {
        transform.position = position;
        transform.rotation = rotation;
        base._id = _id;
        gameObject.SetActive(true);
    }


    // Update is called once per frame

}
