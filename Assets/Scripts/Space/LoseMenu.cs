using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public Button PlayagainButton;
    public Button ExitButton;
    public AudioSource click, Lose;

    public void Play()
    {
        click.Play();
        SceneManager.LoadScene("_Scene_0");
    }

    public void Quit()
    {
        click.Play();
        SceneManager.LoadScene("MainMenu");
    }
}