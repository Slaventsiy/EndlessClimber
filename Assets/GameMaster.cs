using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public Transform playerPrefab;
    public Transform spawnPoint;

    public Text scoreText;

    private static int playerScore = 0;

	// Use this for initialization
	void Start()
    {
	    if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
	}
	
    public void RespawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public static void UpdateScore()
    {
        playerScore++;

        gm.scoreText.text = "Score: " + playerScore;
    }

    public static void EndGame(Player player)
    {
        Destroy(player.gameObject);
        DestroyPlatforms();
        gm.ResetScore();

        PlatformGenerator.Reset();

        gm.RespawnPlayer();
    }

    private void ResetScore()
    {
        scoreText.text = "Score: 0";
        playerScore = 0;
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.RespawnPlayer();
    }

    public static void DestroyPlatforms()
    {
        var clones = GameObject.FindGameObjectsWithTag("Platform");
        foreach (var clone in clones)
        {
            Destroy(clone);
            Debug.Log("Destroyed");
        }
    }
}
