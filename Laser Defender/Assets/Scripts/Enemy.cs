//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter = default;
    [SerializeField] float minShotTime = 0.2f;
    [SerializeField] float maxShotTime = 0.8f;
    [SerializeField] GameObject enemyLaserPrefab = default;
    [SerializeField] float laserSpeed = -10f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minShotTime, maxShotTime);
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            GameObject enemyLaser = Instantiate(
                enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
            enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, laserSpeed);
            Destroy(enemyLaser, 2f);

            shotCounter = Random.Range(minShotTime, maxShotTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
        handleHit(damageDealer);
    }

    private void handleHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
