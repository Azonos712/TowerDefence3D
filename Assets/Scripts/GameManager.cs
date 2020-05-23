using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static bool GameIsOver;

    public GameObject completeLevelUI;
    public AudioSource winAudio;

    void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if (GameIsOver)
            return;

        if (Input.GetKey("e") && Input.GetKey(KeyCode.LeftControl))
        {
            EndGame();
        }

        if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftControl))
        {
            WinLevel();
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
        winAudio.Play();
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
