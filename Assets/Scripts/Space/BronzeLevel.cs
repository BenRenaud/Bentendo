using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BronzeLevel : MonoBehaviour
{
    public Toggle E0B;
    public Toggle E1B;
    public Toggle E2B;
    public Toggle E3B;
    public Toggle E4B;
    public static int NumofE = 2;
    public static int Eselected = 1;
    public static int ScoreToWin = 300;

    public void Awake()
    {
        NumofE = 2;
        ScoreToWin = 300;
        E0B.interactable = false;
        
        if(PlayerPrefs.GetString("E0B").Equals("on"))
        {
            E0B.isOn = true;
            Main.Spawn0B = true;
        }

        if(PlayerPrefs.GetString("E1B").Equals("on"))
        {
            E1B.isOn = true;
            Main.Spawn1B = true;
        }

        if(PlayerPrefs.GetString("E2B").Equals("on"))
        {
            E2B.isOn = true;
            Main.Spawn2B = true;
        }

        if(PlayerPrefs.GetString("E3B").Equals("on"))
        {
            E3B.isOn = true;
            Main.Spawn3B = true;
        }

        if(PlayerPrefs.GetString("E4B").Equals("on"))
        {
            E4B.isOn = true;
            Main.Spawn4B = true;
        }

    }

    public void NumofE_Changed(string newNumofE)
    {
        NumofE = int.Parse(newNumofE);
        Main.MaxEnemies = NumofE;
    }

    public void Score_Change(string newScore)
    {
        ScoreToWin = int.Parse(newScore);
        Main.ScoreToSilver = ScoreToWin;
    }

    public void ToggleE0(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E0B","on");
            Main.Spawn0B = newVal;
            
        }

        else
        {
            PlayerPrefs.SetString("E0B","off");
            Eselected--;
        }
    }

    public void ToggleE1(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E1B","on");
            Main.Spawn1B = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E1B","off");
            Eselected--;
        }
    }

    public void ToggleE2(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E2B","on");
            Main.Spawn2B = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E2B","off");
            Eselected--;
        }
    }

    public void ToggleE3(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E3B","on");
            Main.Spawn3B = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E3B","off");
            Eselected--;
        }
    }

    public void ToggleE4(bool newVal)
    {
        if(newVal)
        {
            Eselected++;
            PlayerPrefs.SetString("E4B","on");
            Main.Spawn4B = newVal;
        }

        else
        {
            PlayerPrefs.SetString("E4B","off");
            Eselected--;
        }
    }
}