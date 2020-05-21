using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;

    LaserPool laserPool = default;

    void Start()
    {
        laserPool = FindObjectOfType<LaserPool>();
    }

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    public void PlayerHit() 
    {
        var poolPosition = laserPool.GetPoolPosition();
        transform.position = poolPosition;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

    }
}
