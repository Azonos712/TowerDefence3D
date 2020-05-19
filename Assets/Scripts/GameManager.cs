using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static bool GameIsOver;

    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    public SceneFader sceneFader;

    void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if (GameIsOver)
            return;

        if (Input.GetKey("e"))
        {
            EndGame();
        }

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        Debug.Log("Level won");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }
}
