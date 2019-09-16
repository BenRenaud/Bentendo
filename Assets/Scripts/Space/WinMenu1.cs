using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu1 : MonoBehaviour
{
    public Button PlayGoldButton;
    public Button ExitButton;
    public AudioSource click;
    public AudioSource winSFX;
    public static AudioClip Win, Win2, Win3;
    public AudioClip[] SpaceWinBG = {Win, Win2, Win3};
    
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
        SceneManager.LoadScene("GoldMainScene");
    }

    public void Quit()
    {
        click.Play();
        SceneManager.LoadScene("MainMenu");
    }
}