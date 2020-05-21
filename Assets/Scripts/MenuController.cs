using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";
    public string nextLevel = "Level02";
    public int levelToUnlock = 2;
    public AudioSource selectAudio;

    string firstLevelToLoad = "LevelSelect";
    public void Continue()
    {
        selectAudio.Play();

        var temp = PlayerPrefs.GetInt("levelReached");
        if (temp < levelToUnlock)
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        selectAudio.Play();
        sceneFader.FadeTo(menuSceneName);
    }

    public void Retry()
    {
        selectAudio.Play();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Play()
    {
        selectAudio.Play();
        sceneFader.FadeTo(firstLevelToLoad);
    }

    public void Qiut()
    {
        selectAudio.Play();
        Debug.Log("Exciting...");
        Application.Quit();
    }
}
