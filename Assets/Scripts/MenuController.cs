using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";
    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    string firstLevelToLoad = "LevelSelect";
    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Play()
    {
        sceneFader.FadeTo(firstLevelToLoad);
    }

    public void Qiut()
    {
        Debug.Log("Exciting...");
        Application.Quit();
    }
}
