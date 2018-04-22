using UnityEngine;

public class Bullet : PoolableObject
{
    PlayerNumber id;
    private Transform target;

    [SerializeField]
    private int damage = 10;

    public void Initialize(Vector3 position, Quaternion rotation, PlayerNumber _id)
    {
        transform.position = position;
        transform.rotation = rotation;
        id = _id;
        gameObject.SetActive(true);
            

    }
    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(collision.gameObject.name + "has collided with " + gameObject.name);
        Player p = collision.gameObject.GetComponent<Player>();
        if (!p)
        {

            //instantiate explosion
            Destroy();
            return;
        }

        if (p.pNumber == id)
        {
            Destroy();
            return;
        }

        p.TakeDamage(damage);

        //Effects etc
        Destroy();

    }
    // Update is called once per frame

}
