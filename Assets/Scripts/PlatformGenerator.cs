using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject PlatformPrefab;

    private Camera camera;
    private int horizontalIndicator = -1;
    private float lastPlatformY = 0;
    private float coefficient = 3;
    private float wallDistance = 7;

    void Awake()
    {
        camera = Camera.main;
    }
    
    void Start()
    {

    }
	
	void Update()
    {
        if (lastPlatformY < camera.transform.position.y + camera.orthographicSize)
        {
            float randValue = coefficient * Random.value;
            float yPos = lastPlatformY + camera.orthographicSize - coefficient / 2 + randValue;
            
            GameObject platform = (GameObject)Instantiate(PlatformPrefab, new Vector3(wallDistance * horizontalIndicator, yPos), camera.transform.rotation);

            platform.transform.SetParent(GameObject.FindGameObjectWithTag("PlatformContainer").GetComponent<Transform>());

            horizontalIndicator *= -1;
            lastPlatformY = yPos;
        }
    }
}
