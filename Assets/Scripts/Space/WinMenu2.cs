using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu2 : MonoBehaviour
{
    public Button PlayGoldButton;
    public Button ExitButton;
    public AudioSource click;
    public AudioSource winSFX;
    public static AudioClip Win, Win2, Win3;
    public AudioClip[] SpaceWinBG = {Win, Win2, Win3};
    public static List<GameLog> Spacelogs = new List<GameLog>();
    void Awake()
    {
        string Spacepath = Application.streamingAssetsPath + "/SpaceLogs.json";
        string SpacejsonString = File.ReadAllText(Spacepath);
        gameDataShooter Spacedata = JsonUtility.FromJson<gameDataShooter>(SpacejsonString);
        Spacelogs.Clear();
        if (!(Spacedata == null))
        {
            foreach (GameLog log in Spacedata.SpaceGame)
            {
                Spacelogs.Add(log);
            }
        }

        int GetWin = PlayerPrefs.GetInt("SpaceBGWin");
        float GetWinV = PlayerPrefs.GetFloat("SpaceBGWinV");
        winSFX.volume = GetWinV;
        winSFX.clip = SpaceWinBG[GetWin];
        winSFX.Play();
        if (Users.CurrentUser.username == "admin")
                {
                    Main.Spacelogs.Add(new GameLog("admin", System.DateTime.Now.ToString(), Main.score.ToString(), Main.lvl));
                } 
                
                else
                {
                    Main.Spacelogs.Add(new GameLog(Users.CurrentUser.username, System.DateTime.Now.ToString(), Main.score.ToString(), Main.lvl));
                }
                
                Main.SaveGameData();
    }

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