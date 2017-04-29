using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject PlatformPrefab;
    public static PlatformGenerator pg;

    private Camera camera;

    private static int horizontalIndicator = -1;
    private static float lastPlatformY = 0;

    private float coefficient = 1;
    private float wallDistance = 7;

    private const float initialLastPlatformY = 0;
    private const int initialHorizontalIndicator = -1;

    private static bool pause = false;

    void Awake()
    {
        camera = Camera.main;
    }

    // Use this for initialization
    void Start()
    {
        if (pg == null)
        {
            pg = GameObject.FindGameObjectWithTag("GM").GetComponent<PlatformGenerator>();
        }
    }

    void Update()
    {
        if (pause)
        {
            if (camera.transform.position.y < 2)
            {
                pause = false;
            }
        }

        if (!pause && lastPlatformY < camera.transform.position.y + camera.orthographicSize)
        {
            float randValue = coefficient * Random.value;
            float yPos = lastPlatformY + camera.orthographicSize - coefficient / 2 + randValue;

            GameObject platform = (GameObject)Instantiate(PlatformPrefab, new Vector3(wallDistance * horizontalIndicator, yPos), camera.transform.rotation);

            platform.transform.SetParent(GameObject.FindGameObjectWithTag("PlatformContainer").GetComponent<Transform>());

            horizontalIndicator *= -1;
            lastPlatformY = yPos;
        }
    }

    public static void Reset()
    {
        pause = true;
        lastPlatformY = initialLastPlatformY;
        horizontalIndicator = initialHorizontalIndicator;
    }

}
