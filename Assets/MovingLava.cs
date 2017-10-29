using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLava : MonoBehaviour {

    public float speed = 0.01f;
    private float maxDistanceFromPlayer = 10f;
    private bool hasStarted = false;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        float bumperCoefficient = 1.1f;
        float lavaSpriteHeight = GetComponent<Renderer>().bounds.size.y;
        maxDistanceFromPlayer = Mathf.Abs(player.transform.position.y - (Camera.main.orthographicSize + Camera.main.transform.position.y)) + lavaSpriteHeight;
        maxDistanceFromPlayer *= bumperCoefficient;
    }

    void Awake()
    {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if (hasStarted)
        {
            UpdatePosition();
        }

    }

    public void StartLava()
    {
        hasStarted = true;
    }

    void UpdatePosition()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 newPosition = new Vector2(transform.position.x, 0);

        float distanceFromPlayer = Mathf.Abs(transform.position.y - playerPosition.y);
        if (distanceFromPlayer > maxDistanceFromPlayer)
        {
            newPosition.y = playerPosition.y - maxDistanceFromPlayer;
        }
        else
        {
            newPosition.y = transform.position.y + speed;
        }

        transform.position = newPosition;
    }
}
