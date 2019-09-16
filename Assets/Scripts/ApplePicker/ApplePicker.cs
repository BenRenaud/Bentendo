using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour{
    [Header("Set in Inspector")]
    public static List<GameLog> logs = new List<GameLog>();
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    public AudioSource Lose, Click;

    void Start(){
 
        string path = Application.streamingAssetsPath + "/AppleLogs.json";
        string jsonString = File.ReadAllText(path);
        gameData data = JsonUtility.FromJson<gameData>(jsonString);
        logs.Clear();
        if (!(data == null))
        {
            foreach (GameLog log in data.ApplePicker)
            {
                logs.Add(log);
            }
        }

        basketList = new List<GameObject>();
        for(int i = 0; i < numBaskets; i++){
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    public static List<GameLog> GetLogs()
    {
        return logs;
    }

    public void AppleDestroyed(){
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach(GameObject tGO in tAppleArray){
        Destroy(tGO);
        }
    Lose.Play();
    int basketIndex = basketList.Count - 1;
    GameObject tBasketGO = basketList[basketIndex];
    basketList.RemoveAt(basketIndex);
    Destroy(tBasketGO);

    if(basketList.Count == 0)
    {
        if (Users.CurrentUser.username == "admin")
            {
                ApplePicker.logs.Add(new GameLog("admin", System.DateTime.Now.ToString(), Basket.StaticScore.ToString(), "n/a")); // game log for admin
            } 
            
            else 
            {
                ApplePicker.logs.Add(new GameLog(Users.CurrentUser.username, System.DateTime.Now.ToString(), Basket.StaticScore.ToString(), "n/a")); // game log for player
            }
        SaveGameDataApple();
        SceneManager.LoadScene("ApplePickerGame");
    }
}

public void SaveGameDataApple() // save the data of the player
    {
        gameData newData = new gameData();
        string path = Application.streamingAssetsPath + "/AppleLogs.json";
        foreach (GameLog log in ApplePicker.logs) // for each log in Apple pickers logs
        {
            newData.Add(log); // add the new game data
        }
        File.WriteAllText(path, JsonUtility.ToJson(newData)); // write log into system
    }

    public void ExitGame()
    {
        Click.Play();
        SceneManager.LoadScene("MainMenu");
    }
}

[System.Serializable]
public class gameData // create a game data object for apple picker to hold the game log of users
{
    public List<GameLog> ApplePicker = new List<GameLog>();

    public void Add(GameLog log) // add a game log
    {
        ApplePicker.Add(log);
    }
}

[System.Serializable]
public class GameLog // create a game log object to be used to store data for all of the games
{
    public string Username; // username of player
    public string Date; // date the player is playing
    public string Score; // score of the player
    public string HighestLevel; // highest level in space-shooter

    public GameLog(string u, string d, string s, string h) // overloaded constructor
    {
        this.Username = u;
        this.Date = d;
        this.Score = s;
        this.HighestLevel = h;
    }
}
