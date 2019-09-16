using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource Click;
    public Button Button12;
    public Button Button14;
    public Button Button16;
    public Button Button18;
    public Button Button20;
    public Button BackButton;

    public void Back()
    {
        Click.Play();
        SceneManager.LoadScene("MemMainMenu");
    }

    public void B12()
    {
        Click.Play();
        Buttons.NumOfCards = 12;
    }

    public void B14()
    {
        Click.Play();
        Buttons.NumOfCards = 14;
    }

    public void B16()
    {
        Click.Play();
        Buttons.NumOfCards = 16;
    }

    public void B18()
    {
        Click.Play();
        Buttons.NumOfCards = 18;
    }

    public void B20()
    {
        Click.Play();
        Buttons.NumOfCards = 20;
    }
}