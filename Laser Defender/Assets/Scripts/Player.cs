using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Config params
    [Header("Player Stats")]
    [SerializeField] int health = 500;
    [SerializeField] [Range(5f,30f)] float moveSpeed = 15f;
    [SerializeField] float padding = 0.6f;

    [Header("Player Projectile")]
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float firingPeriod = 0.1f;

    [Header("Player Effects")]
    [SerializeField] AudioClip laserSFX = default;
    [Range(0, 1)][SerializeField] float laserVolume = 0.3f;
    [SerializeField] AudioClip deathSFX = default;
    [Range(0, 1)] [SerializeField] float deathVolume = 1f;
    [SerializeField] GameObject deathVFX = default;
    [SerializeField] float explosionDuration = 0.5f;

    //Cached reference
    private GameObject[] laserPool = default;
    private HealthBar healthBar = default;

    private int laserIndex = 0;
    private Vector3 camMin;
    private Vector3 camMax;


    void Start()
    {
        SetupMoveBoundaries();
        laserPool = FindObjectOfType<LaserPool>().GetPlayerLaserPool();
        healthBar = FindObjectOfType<HealthBar>();
    }

    void Update()
    {
        Move();
        Fire();
    }

    //Handle player shooting
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(FireContinuosly());
        }
    }
    private IEnumerator FireContinuosly()
    {
        WaitForSeconds fireDelay = new WaitForSeconds(firingPeriod);

        while (Input.GetButton("Fire1"))
        {
            GameObject laser = laserPool[laserIndex];
            laser.transform.position = transform.position;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, laserSpeed);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserVolume);


            //Restart the index
            laserIndex++;
            if (laserIndex >= laserPool.Length)
            {
                laserIndex = 0;
            }

            yield return fireDelay;
        }
    }

    //Handle enemy lasers and collisions
    private void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        HandleHit(damageDealer);
    }
    private void HandleHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (healthBar != null)
        {
            healthBar.UpdateHealth(health);
        }
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
            

        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
        GameObject explosion = Instantiate(
            deathVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, explosionDuration);
    }

    public int GetHealth()
    {
        return health;
    }

    //Handle player movement
    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float newPosX = Mathf.Clamp(transform.position.x + deltaX, camMin.x + padding, camMax.x - padding);
        float newPosY = Mathf.Clamp(transform.position.y + deltaY, camMin.y + padding, camMax.y - padding);

        transform.position = new Vector2(newPosX, newPosY);
    }
    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        camMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        camMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));
    }

}
