using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SilverLevel : MonoBehaviour
{
    public Button ExitButton;
    public Toggle E0S;
    public Toggle E1S;
    public Toggle E2S;
    public Toggle E3S;
    public Toggle E4S;
    public static int NumofESilver = 4;
    public static int Eselected = 2;
    public static int ScoreToWinSilver = 500;
    public AudioSource click;

    public void Quit()
    {
        click.Play();
        SceneManager.LoadScene("GameLevelsMenu");
    }

    public void Awake()
    {
        NumofESilver = 4;
        ScoreToWinSilver = 500;
        E2S.interactable = false;
        E3S.interactable = false;

        if(PlayerPrefs.GetString("E0S").Equals("on"))
        {
            E0S.isOn = true;
            Main.Spawn0S = true;
        }

        if(PlayerPrefs.GetString("E1S").Equals("on"))
        {
            E1S.isOn = true;
            Main.Spawn1S = true;
        }

        if(PlayerPrefs.GetString("E2S").Equals("on"))
        {
            E2S.isOn = true;
            Main.Spawn2S = true;
        }

        if(PlayerPrefs.GetString("E3S").Equals("on"))
        {
            E3S.isOn = true;
            Main.Spawn3S = true;
        }

        if(PlayerPrefs.GetString("E4S").Equals("on"))
        {
            E4S.isOn = true;
            Main.Spawn4S = true;
        }
    }

    public void NumofE_Changed(string newNumofE)
    {
        NumofESilver = int.Parse(newNumofE);
        Main.MaxEnemies = NumofESilver;
    }

    public void Score_Change(string newScore)
    {
        ScoreToWinSilver = int.Parse(newScore);
        Main.ScoreToGold = ScoreToWinSilver;
    }

    public void ToggleE0(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E0S","on");
            Main.Spawn0S = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E0S","off");
            Eselected--;
        }
    }

    public void ToggleE1(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E1S","on");
            Main.Spawn1S = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E1S","off");
            Eselected--;
        }
    }

    public void ToggleE2(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E2S","on");
            Main.Spawn2S = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E2S","off");
            Eselected--;
        }
    }

    public void ToggleE3(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E3S","on");
            Main.Spawn3S = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E3S","off");
            Eselected--;
        }
    }

    public void ToggleE4(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E4S","on");
            Main.Spawn4S = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E4S","off");
            Eselected--;
        }
    }
}