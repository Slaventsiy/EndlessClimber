using UnityEngine;
using System;
using System.Collections;

public class ArrowMove : MonoBehaviour
{
    public float speed = 100;

    private int maxAngle = 60;

    void Start()
    {

    }
    
    void Update()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.PingPong(Time.time * speed, maxAngle)));

        Debug.Log("MOOOVE");
                
    }
}
