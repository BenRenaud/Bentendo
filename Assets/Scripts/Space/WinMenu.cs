using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public Button PlaySilverButton;
    public AudioSource winSFX;
    public static AudioClip Win, Win2, Win3;
    public AudioClip[] SpaceWinBG = {Win, Win2, Win3};
    public Button ExitButton;
    public AudioSource click;

    void Awake()
    {
        int GetWin = PlayerPrefs.GetInt("SpaceBGWin");
        float GetWinV = PlayerPrefs.GetFloat("SpaceBGWinV");
        winSFX.volume = GetWinV;
        winSFX.clip = SpaceWinBG[GetWin];
        winSFX.Play();
    }

    public void Play()
    {
        click.Play();
        SceneManager.LoadScene("SilverMainScene");
    }

    public void Quit()
    {
        click.Play();
        SceneManager.LoadScene("MainMenu");
    }
}