//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100f;
    [SerializeField] GameObject enemyLaserPrefab = default;

    [Header("Enemy Projectile")]
    [SerializeField] float minShotTime = 0.2f;
    [SerializeField] float maxShotTime = 0.8f;
    [SerializeField] float laserSpeed = -10f;

    [Header("Enemy Effects")]
    [SerializeField] AudioClip laserSFX = default;
    [Range(0, 1)] [SerializeField] float laserVolume = 0.3f;
    [SerializeField] AudioClip deathSFX = default;
    [Range(0, 1)] [SerializeField] float deathVolume = 1f;
    [SerializeField] GameObject deathVFX = default;
    [SerializeField] float explosionDuration = 0.5f;

    private float shotCounter = default;

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
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserVolume);
            Destroy(enemyLaser, 2f);

            shotCounter = Random.Range(minShotTime, maxShotTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        HandleHit(damageDealer);
    }

    private void HandleHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.PlayerHit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
        GameObject explosion = Instantiate(
            deathVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, explosionDuration);
    }
}
