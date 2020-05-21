using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config params
    [Header("Player Stats")]
    [SerializeField] float health = 500f;
    [SerializeField] [Range(5f,30f)] float moveSpeed = 15f;
    [SerializeField] float padding = 0.6f;

    [Header("Player Projectile")]
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float firingPeriod = 0.1f;

    private GameObject[] laserPool = default;
    private int laserIndex = 0;
    Vector3 camMin;
    Vector3 camMax;


    void Start()
    {
        SetupMoveBoundaries();
        laserPool = FindObjectOfType<LaserPool>().GetPlayerLaserPool();
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
        handleHit(damageDealer);
    }
    private void handleHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
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
