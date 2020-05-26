using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float spinSpeed = 360f;
    void Update()
    {
        transform.Rotate(0f, 0f, spinSpeed);
    }
}
