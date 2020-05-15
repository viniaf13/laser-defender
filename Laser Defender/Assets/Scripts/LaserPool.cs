using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    //Config params
    [SerializeField] int laserPoolSize = 15;
    [SerializeField] GameObject laserPrefab = default;
    [SerializeField] Vector2 objectPoolPos = new Vector2(0f, -10f);

    private GameObject[] lasers;

    void Awake()
    {
        //Spawn lasers outside camera
        lasers = new GameObject[laserPoolSize];
        for (int i = 0; i < laserPoolSize; i++)
        {
            lasers[i] = Instantiate(laserPrefab, objectPoolPos, Quaternion.identity);
        }

    }

    //Get access to the laser pool
    public GameObject[] GetLaserPool()
    {
        return lasers;
    }
}
