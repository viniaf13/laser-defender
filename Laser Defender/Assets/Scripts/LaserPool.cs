using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    //Config params
    [SerializeField] int laserPoolSize = 15;
    [SerializeField] GameObject playerLaserPrefab = default;
    [SerializeField] Vector2 objectPoolPos = new Vector2(0f, -10f);

    private GameObject[] playerLasers;

    void Awake()
    {
        //Spawn lasers outside camera
        playerLasers = new GameObject[laserPoolSize];
 
        for (int i = 0; i < laserPoolSize; i++)
        {
            playerLasers[i] = Instantiate(playerLaserPrefab, objectPoolPos, Quaternion.identity) as GameObject;
        }

    }

    //Get access to the laser pool
    public GameObject[] GetPlayerLaserPool()
    {
        return playerLasers;
    }
}
