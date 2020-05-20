using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";
    public AudioSource selectAudio;
    public AudioSource backAudio;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        //Остановка времени
        if (ui.activeSelf)
        {
            backAudio.Pause();
            Time.timeScale = 0f;
        }
        else
        {
            selectAudio.Play();
            backAudio.UnPause();
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        selectAudio.Play();
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        selectAudio.Play();
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }
}
