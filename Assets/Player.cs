using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(transform.position.x) > 7.5)
        {
            GameMaster.EndGame(this);
        }
    }
}
