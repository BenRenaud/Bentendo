using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoldLevel : MonoBehaviour
{
    public Button ExitButton;
    public Toggle E0G;
    public Toggle E1G;
    public Toggle E2G;
    public Toggle E3G;
    public Toggle E4G;
    public static int NumofEGold = 6;
    public static int Eselected = 3;
    public static int ScoreToWinGold = 700;
    public AudioSource click;

    public void Quit()
    {
        click.Play();
        SceneManager.LoadScene("GameLevelsMenu");
    }

    public void Awake()
    {
        NumofEGold = 6;
        ScoreToWinGold = 700;
        E4G.interactable = false;
        E3G.interactable = false;
        E0G.interactable = false;
        
        if(PlayerPrefs.GetString("E0G").Equals("on"))
        {
            E0G.isOn = true;
            Main.Spawn0G = true;
        }

        if(PlayerPrefs.GetString("E1G").Equals("on"))
        {
            E1G.isOn = true;
            Main.Spawn1G = true;
        }

        if(PlayerPrefs.GetString("E2G").Equals("on"))
        {
            E2G.isOn = true;
            Main.Spawn2G = true;
        }

        if(PlayerPrefs.GetString("E3G").Equals("on"))
        {
            E3G.isOn = true;
            Main.Spawn3G = true;
        }

        if(PlayerPrefs.GetString("E4G").Equals("on"))
        {
            E4G.isOn = true;
            Main.Spawn4G = true;
        }

    }

    public void NumofE_Changed(string newNumofE)
    {
        NumofEGold = int.Parse(newNumofE);
        Main.MaxEnemies = NumofEGold;
    }

    public void Score_Change(string newScore)
    {
        ScoreToWinGold = int.Parse(newScore);
        Main.ScoreToBeatGame = ScoreToWinGold;
    }

    public void ToggleE0(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E0G","on");
            Main.Spawn0G = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E0G","off");
            Eselected--;
        }
    }

    public void ToggleE1(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E1G","on");
            Main.Spawn1G = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E1G","off");
            Eselected--;
        }
    }

    public void ToggleE2(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E2G","on");
            Main.Spawn2G = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E2G","off");
            Eselected--;
        }
    }

    public void ToggleE3(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E3G","on");
            Main.Spawn3G = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E3G","off");
            Eselected--;
        }
    }

    public void ToggleE4(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E4G","on");
            Main.Spawn4G = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E4G","off");
            Eselected--;
        }
    }
}