using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnitySampleAssets._2D;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public Transform playerPrefab;
    public Transform spawnPoint;

    public Text scoreText;
    public Text finalScoreText;
    public GameObject GameOverUI;

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

    public static void ActivateGameOverScreen()
    {
        // Load the game over layout
        gm.GameOverUI.SetActive(true);
        gm.finalScoreText.text = "Score: " + playerScore;       
    }

    public static void Reset()
    {
        PlatformGenerator.Reset();
        gm.ResetScore();
    }

    public void ResetScore()
    {
        scoreText.text = "Score: 0";
        playerScore = 0;
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.RespawnPlayer();
    }
}
