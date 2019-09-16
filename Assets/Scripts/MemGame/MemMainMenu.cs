using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MemMainMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button ExitButton;
    public Button SettingsButton;
    public AudioSource Click;

    public void Play()
    {
        Click.Play();
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Click.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void Settings()
    {
        Click.Play();
        SceneManager.LoadScene("SettingScene");
    }

}