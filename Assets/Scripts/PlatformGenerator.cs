using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour
{
    public Transform PlatformPrefab;

    private Camera camera;
    private int horizontalIndicator = -1;
    private float lastPlatformY = 0;

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
            float yPos = lastPlatformY + camera.orthographicSize + 0;
            Instantiate(PlatformPrefab, new Vector3(7 * horizontalIndicator, yPos), camera.transform.rotation);

            horizontalIndicator *= -1;
            lastPlatformY = yPos;
        }
    }
}
