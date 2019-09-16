using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLoseMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button ExitButton;
    public Text EndScore;
    public Text EndTime;
    public AudioSource Click;

    void Start()
    {
       PrintResults();
       if(Application.loadedLevelName == "WinScene") // if player won
       {
           if (Users.CurrentUser.username == "admin")
            {
                MainMenu.Memlogs.Add(new GameLog("admin", System.DateTime.Now.ToString(), GameController.score.ToString(), "n/a")); // log admin data into system
            } 
                
            else
            {
                MainMenu.Memlogs.Add(new GameLog(Users.CurrentUser.username, System.DateTime.Now.ToString(), GameController.score.ToString(), "n/a")); // log player data into system
            }
            SaveGameData();
       }
    }
    public void Play()
    {
        Click.Play();
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Click.Play();
        SceneManager.LoadScene("MemMainMenu");
    }

    public static void SaveGameData()
    {
        gameDataMemory newData = new gameDataMemory();
        string Mempath = Application.streamingAssetsPath + "/MemLogs.json";
        foreach (GameLog log in MainMenu.Memlogs)
        {
            newData.Add(log);
        }
        File.WriteAllText(Mempath, JsonUtility.ToJson(newData));
    }

    public void PrintResults()
    {
        EndScore.text = "Score = " + GameController.score;
        EndTime.text = "Time Elapsed: " + Mathf.Round(GameController.time * 1f) / 1f + "s";
    }
}

[System.Serializable]
public class gameDataMemory
{
    public List<GameLog> MemoryGame = new List<GameLog>();

    public void Add(GameLog log)
    {
        MemoryGame.Add(log);
    }
}