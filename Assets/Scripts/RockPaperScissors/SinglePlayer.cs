using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SinglePlayer: MonoBehaviour {
    public Button rock;
    public Button paper;
    public Button scissors;
    public GameObject draw;
    public GameObject win;
    public GameObject lose;
    public GameObject winner1;
    public GameObject winner2;
    int random, newrandom;
    static int p1w = 0;
    static int p2w = 0;
    public Image CompChoice, RockPic, PaperPic, ScissorsPic;
    public AudioSource WinSound, LoseSound, ClickGame, ClickExit;
    public static List<GameLog> logs = new List<GameLog>();

    void Start () 
    {
        string RPSpath = Application.streamingAssetsPath + "/RPSLogs.json";
        string RPSjsonString = File.ReadAllText(RPSpath);
        gameDataRPS RPSdata = JsonUtility.FromJson<gameDataRPS>(RPSjsonString);
        logs.Clear();
        if (!(RPSdata == null))
        {
            foreach (GameLog log in RPSdata.RockPaperScissors)
            {
                logs.Add(log);
            }
        }
    }

    void Awake() 
    {
        draw.gameObject.SetActive(false);
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
        winner1.gameObject.SetActive(false);
        winner2.gameObject.SetActive(false);
    }

    void Update()
    {
        random = Random.Range(1, 4);
       
        if(p1w == 10)
        {
            
            win.SetActive(false);
            lose.SetActive(false);
            draw.SetActive(false);
            rock.interactable = false;
            paper.interactable = false;
            scissors.interactable = false;
            winner1.SetActive(true);
        }

        if(p2w == 10)
        {
            win.SetActive(false);
            lose.SetActive(false);
            draw.SetActive(false);
            rock.interactable = false;
            paper.interactable = false;
            scissors.interactable = false;
            winner2.SetActive(true);
        }
    }

    void P1win()
    {
        rock.interactable = false;
        paper.interactable = false;
        scissors.interactable = false;
        win.gameObject.SetActive(true);
        p1w++;
        PlayerScore.p1score = p1w;

        if(p1w == 10)
        {
            WinSound.Play();
            if (Users.CurrentUser.username == "admin") 
            {
                logs.Add(new GameLog("admin", System.DateTime.Now.ToString(), "Player", "n/a")); // log admin data into system
            } 
            
            else
            {
                logs.Add(new GameLog(Users.CurrentUser.username, System.DateTime.Now.ToString(), "Player", "n/a")); // log player data into system
            }
            SaveGameData();
        }
    }
    
    void P2win()
    {
        rock.interactable = false;
        paper.interactable = false;
        scissors.interactable = false;
        lose.gameObject.SetActive(true);
        p2w++;
        CompScore.p2score = p2w;

        if(p2w == 10)
        {
            LoseSound.Play();
            if (Users.CurrentUser.username == "admin") 
            {
                logs.Add(new GameLog("admin", System.DateTime.Now.ToString(), "Robot", "n/a")); // comp won, log admin data into system
            } 
            
            else
            {
                logs.Add(new GameLog(Users.CurrentUser.username, System.DateTime.Now.ToString(), "Robot", "n/a")); // comp won, log player daya into system
            }
            SaveGameData();
        }
    }
    void Draw()
    {
        rock.interactable = false;
        paper.interactable = false;
        scissors.interactable = false;
        draw.gameObject.SetActive(true);
    }

    public void Rock()
    {
        ClickGame.Play();

        if(random == 1)
        {
            Draw();
            CompChoice.sprite = RockPic.sprite;
        }

        else if(random == 2)
        {
            CompChoice.sprite = PaperPic.sprite;
            P2win();
        }

        else if(random == 3)
        {
            CompChoice.sprite = ScissorsPic.sprite;
            P1win();
        }

    }
    public void Paper()
    {
        ClickGame.Play();

        if(random == 2)
        {
            CompChoice.sprite = PaperPic.sprite;
            Draw();
        }
        else if(random == 1)
        {
            CompChoice.sprite = RockPic.sprite;
            P1win();
        }
        else if(random == 3)
        {
            CompChoice.sprite = ScissorsPic.sprite;
            P2win();
        }

    }
    public void Scissors()
    {
        ClickGame.Play();

        if(random == 3)
        {
            CompChoice.sprite = ScissorsPic.sprite;
            Draw();
        }
        else if(random == 2)
        {
            CompChoice.sprite = PaperPic.sprite;
            P1win();
        }
        else if(random == 1)
        {
            CompChoice.sprite = RockPic.sprite;
            P2win();
        }

    }

    public void LeavePannel1()
    {
        ClickExit.Play();
        rock.interactable = true;
        paper.interactable = true;
        scissors.interactable = true;
        win.SetActive(false);
    }

    public void LeavePannel2()
    {
        ClickExit.Play();
        rock.interactable = true;
        paper.interactable = true;
        scissors.interactable = true;
        lose.SetActive(false);
    }

    public void LeavePannel3()
    {
        ClickExit.Play();
        rock.interactable = true;
        paper.interactable = true;
        scissors.interactable = true;
        draw.SetActive(false);
    }

    public void LeavePannel4()
    {
        ClickExit.Play();
        PlayerScore.p1score = 0;
        CompScore.p2score = 0;
        p1w = 0;
        p2w = 0;
        rock.interactable = true;
        paper.interactable = true;
        scissors.interactable = true;
        winner1.SetActive(false);
    }

    public void LeavePannel5()
    {
        ClickExit.Play();
        PlayerScore.p1score = 0;
        CompScore.p2score = 0;
        p1w = 0;
        p2w = 0;
        rock.interactable = true;
        paper.interactable = true;
        scissors.interactable = true;
        winner2.SetActive(false);
    }

    public void SaveGameData()
    {
        gameDataRPS newData = new gameDataRPS();
        string path = Application.streamingAssetsPath + "/RPSLogs.json";
        foreach (GameLog log in logs)
        {
            newData.Add(log);
        }
        File.WriteAllText(path, JsonUtility.ToJson(newData));
    }
    
    public void Exit()
    {
        ClickExit.Play();
        SceneManager.LoadScene("MainMenu");
    } 
}

[System.Serializable]
public class gameDataRPS // create object for game data for rockpaperscissors
{
    public List<GameLog> RockPaperScissors = new List<GameLog>();
    public void Add(GameLog log)
    {
        RockPaperScissors.Add(log);
    }
}