using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : ShootingObject, IHealthInterface, ITarget
{

    public float lapCompleteHealthBoost = 5;

    [Header("Sprites")]
    public Sprite towerAlive;
    public Sprite towerDead;
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer towerSprite;

    private float _totalHealth = 200;
    private float _currentHealth;

    public float TotalHealth
    {
        get
        {
            return _totalHealth;
        }

        set
        {
            _totalHealth = value;
        }
    }
    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }

        set
        {
            _currentHealth = value;
        }
    }

    public Transform Position
    {
        get
        {
            return transform;
        }

    }

    public PlayerNumber PlayerNUmber
    {
        get
        {
            return _id;
        }

    }
    public bool isAlive = true;


    public override void Initialize(Vector3 position, PlayerNumber _id)
    {
        base.Initialize(position, _id);
        _currentHealth = _totalHealth;
    }


    protected override void Update()
    {
        if (!isAlive)
        {
            return;
        }
        base.Update();
        Debug.Log("CUrrent Health : " + CurrentHealth);
    }

    private void Start()
    {
       GetComponent<CircleCollider2D>().radius = range;
        _currentHealth = _totalHealth;
        GameEventHandler.OnLapComplete += OnLapComplete;
    }

    protected override void Shoot()
    {
        GameObject missileGO = ObjectPooler.Instance.GetPooledGameObject(PooledObjectType.Missile);
        Missile missile = missileGO.GetComponent<Missile>();
        missile.Initialize(transform.position, partToRotate.rotation, _id, target.position);
        Rigidbody2D missileRB = missileGO.GetComponent<Rigidbody2D>();
        missileRB.velocity = partToRotate.TransformDirection(transform.right * fireForce);

        AudioManager.Instance.PlaySound(shootingSFX, transform.position);
    }

    public void OnLapComplete(Player player)
    {
        if (player._id != _id)
        {
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth + lapCompleteHealthBoost, 0, _totalHealth);


    }

    public void TakeDamage(int damage, PlayerNumber number)
    {
        if (_id == number)
        {
            return;
        }

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }
    private void Die()
    {
        towerSprite.sprite = towerDead;


        isAlive = false;

        GameEventHandler.CallOnTowerDeath(this);
    }


    public void DisableColliders()
    {
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
        CircleCollider2D cc = GetComponent<CircleCollider2D>();
        cc.enabled = false;
    }
}
