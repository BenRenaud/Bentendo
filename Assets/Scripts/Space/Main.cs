using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    static public Main S;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public List<GameObject> Wanted = new List<GameObject>();
    public static bool Spawn0B = true,  Spawn1B = false;
    public static bool Spawn4B = false, Spawn2B = false, Spawn3B = false;
    public static bool Spawn2S = true,  Spawn3S = true, Spawn0S = false;
    public static bool Spawn1S = false, Spawn4S = false;
    public static bool Spawn4G = true,  Spawn3G = true, Spawn1G = false, Spawn0G = true, Spawn2G = false;
    public static int MaxEnemies = 2;
    public static int size;
    public static int score = 0;
    public static float time;
    public Text TimeText;
    public Text ScoreText;
    public string minutes;
    public string seconds;
    public static int ScoreToSilver = 300;
    public static int ScoreToGold = 500;
    public static int E0Score = 25, E1Score = 50, E2Score = 75, E3Score = 100, E4Score = 100;
    public static int ScoreToBeatGame = 700;
    public static int E0kills, E1kills, E2kills, E3kills, E4kills;
    public Text E0Text, E1Text, E2Text, E3Text, E4Text;
    public static string E0color = "white", E1color = "white", E2color = "white", E3color = "white", E4color = "white";
    public bool paused;
    public Button pauseButton, resumeButton, restartButton, mainmenuButton;
    public static int NumofEonScreen = 0;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;
    public Material[] BackroundMaterials;
    public GameObject Backroundimage;
    public Material[] materials;
    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public GameObject PauseMenu;
    public Image PauseBG;
    public AudioSource click, BoomSFX;
    public AudioSource MusicBG;
    public static AudioClip StarMaze, SpaceLand, SpaceMusic, BowersCastle, Boom, Boom2, Boom3;
    public AudioClip[] SpaceMusicBG = {StarMaze, SpaceLand, SpaceMusic, BowersCastle};
    public AudioClip[] SpaceBoomBG = {Boom, Boom2, Boom3};
    public WeaponType[] powerUpFrequency = new WeaponType[] {
    WeaponType.blaster, WeaponType.blaster, WeaponType.spread, WeaponType.shield
    };
    public static List<GameLog> Spacelogs = new List<GameLog>();
    public static string lvl = "Bronze";
    private BoundsCheck bndCheck;

    public void shipDestroyed(Enemy e){
        NumofEonScreen--;
        BoomSFX.Play();
        switch(e.name)
        {
            case "Enemy_0(Clone)":
            Main.E0kills += 1;
            score += E0Score;
            break;

            case "Enemy_1(Clone)":
            Main.E1kills += 1;
            score += E1Score;
            break;

            case "Enemy_2(Clone)":
            score += E2Score;
            break;

            case "Enemy_3(Clone)":
            Main.E3kills += 1;
            score += E3Score;
            break;

            case "Enemy_4(Clone)":
            score += E4Score;
            break;
        }
        if(Random.value <= e.powerUpDropChance){
            int ndx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[ndx];

            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();
            pu.SetType(puType);
            pu.transform.position = e.transform.position;
        }
    }
   
    void Awake()
    {
        Backroundimage.GetComponent<Renderer>().material = BackroundMaterials[PlayerPrefs.GetInt("index")];
        int GetMusic = PlayerPrefs.GetInt("SpaceBG");
        float GetMusicV = PlayerPrefs.GetFloat("SpaceBGV");
        MusicBG.volume = GetMusicV;
        MusicBG.clip = SpaceMusicBG[GetMusic];
        int GetBoom = PlayerPrefs.GetInt("SpaceBGBoom");
        float GetBoomV = PlayerPrefs.GetFloat("SpaceBGBoomV");
        BoomSFX.volume = GetBoomV;
        BoomSFX.clip = SpaceBoomBG[GetBoom];
        lvl = "Bronze";
        MusicBG.Play();
        score = 0;
        time = 0;
        E0kills = 0;
        E1kills = 0;
        E2kills = 0;
        E3kills = 0;
        E4kills = 0;
        size = 0;
        S = this;
        paused = false;
        resumeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        mainmenuButton.gameObject.SetActive(false);
        PauseBG.gameObject.SetActive(false);
        NumofEonScreen = 0;
        MaxEnemies = BronzeLevel.NumofE;
        ScoreToSilver = BronzeLevel.ScoreToWin;
        float GetX = PlayerPrefs.GetFloat("X");
        float GetY = PlayerPrefs.GetFloat("Y");
        Backroundimage.transform.localScale = new Vector3(GetX, GetY, 1);
        if(Application.loadedLevelName == "SilverMainScene")
        {
            Backroundimage.GetComponent<Renderer>().material = BackroundMaterials[PlayerPrefs.GetInt("indexSilver")];
            lvl = "Silver";
            MaxEnemies = SilverLevel.NumofESilver;
            ScoreToGold = SilverLevel.ScoreToWinSilver;
        }
        if(Application.loadedLevelName == "GoldMainScene")
        {
            Backroundimage.GetComponent<Renderer>().material = BackroundMaterials[PlayerPrefs.GetInt("indexGold")];
            lvl = "Gold";
            MaxEnemies = GoldLevel.NumofEGold;
            ScoreToBeatGame = GoldLevel.ScoreToWinGold;
        }
        bndCheck = GetComponent<BoundsCheck>();
        fillArray();
        Invoke("SpawnEnemy", 1f/enemySpawnPerSecond);

        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach(WeaponDefinition def in weaponDefinitions){
            WEAP_DICT[def.type] = def;
        }
    }

    void Start()
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
    }

    void Update()
    {
        time = time + Time.deltaTime;
        ScoreText.text = "Score: " + score;
        minutes = ((float)time/1.00f).ToString();
        seconds = ((Mathf.Round((time)*1f)/1f) % 60).ToString("2f");
        TimeText.text = minutes + "." + seconds;
        E0Text.text = "E0 kills: " + E0kills;
        E1Text.text = "E1 kills: " + E1kills;
        E2Text.text = "E2 kills: " + E2kills;
        E3Text.text = "E3 kills: " + E3kills;
        E4Text.text = "E4 kills: " + E4kills;


        if(score >= ScoreToSilver && Application.loadedLevelName == "_Scene_0")
        {
            SceneManager.LoadScene("SpaceWinScene");
        }

        else if(score >= ScoreToGold && Application.loadedLevelName == "SilverMainScene")
        {
            SceneManager.LoadScene("WinScene1");
        }

        else if(score >= ScoreToBeatGame && Application.loadedLevelName == "GoldMainScene")
        {
            SceneManager.LoadScene("WinScene2");
        }
    }

    public void pauseGame()
    {
        click.Play();
        paused = !paused;

        if(paused)
        {
            pauseButton.gameObject.SetActive(false);
            resumeButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            mainmenuButton.gameObject.SetActive(true);
            PauseBG.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        if(!paused)
        {
            pauseButton.gameObject.SetActive(true);
            resumeButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            mainmenuButton.gameObject.SetActive(false);
            PauseBG.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SpawnEnemy() 
    {
        if(NumofEonScreen < MaxEnemies){
        int ndx = Random.Range(0, size);
        GameObject go = Instantiate<GameObject>(Wanted[ndx]);
        ChangeColor(go);

        float enemyPadding = enemyDefaultPadding;
        if(go.GetComponent<BoundsCheck>() != null) {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;
        NumofEonScreen++;
        }
        Invoke("SpawnEnemy", 1f/enemySpawnPerSecond);
    }

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        Destroy(this);
        SceneManager.LoadScene("_Scene_0");
    }

    public void RestartB()
    {
        click.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("_Scene_0");
    }
    public void Quit()
    {
        click.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType wt){
        if(WEAP_DICT.ContainsKey(wt)){
            return(WEAP_DICT[wt]);
        }
        return (new WeaponDefinition());
    }

    public void fillArray()
    {
        if(Application.loadedLevelName == "_Scene_0")
        {
        if(Spawn0B)
        {
            size++;
            Wanted.Add(prefabEnemies[0]);
        }

        if(Spawn1B)
        {
            size++;
            Wanted.Add(prefabEnemies[1]);
        }

        if(Spawn2B)
        {
            size++;
            Wanted.Add(prefabEnemies[2]);
        }

        if(Spawn3B)
        {
            size++;
            Wanted.Add(prefabEnemies[3]);
        }

        if(Spawn4B)
        {
            size++;
            Wanted.Add(prefabEnemies[4]);
        }
        }

        if(Application.loadedLevelName == "SilverMainScene")
        {
        if(Spawn0S)
        {
            size++;
            Wanted.Add(prefabEnemies[0]);
        }

        if(Spawn1S)
        {
            size++;
            Wanted.Add(prefabEnemies[1]);
        }

        if(Spawn2S)
        {
            size++;
            Wanted.Add(prefabEnemies[2]);
        }

        if(Spawn3S)
        {
            size++;
            Wanted.Add(prefabEnemies[3]);
        }

        if(Spawn4S)
        {
            size++;
            Wanted.Add(prefabEnemies[4]);
        }

        }

        if(Application.loadedLevelName == "GoldMainScene")
        {
            if(Spawn0G)
        {
            size++;
            Wanted.Add(prefabEnemies[0]);
        }

        if(Spawn1G)
        {
            size++;
            Wanted.Add(prefabEnemies[1]);
        }

        if(Spawn2G)
        {
            size++;
            Wanted.Add(prefabEnemies[2]);
        }

        if(Spawn3G)
        {
            size++;
            Wanted.Add(prefabEnemies[3]);
        }

        if(Spawn4G)
        {
            size++;
            Wanted.Add(prefabEnemies[4]);
        }

        }
    }

    public void ChangeColor(GameObject go)
    {
        if(go.name.Equals("Enemy_0(Clone)"))
        {

        if(E0color.Equals("white"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.white;
        }
        }

        if(E0color.Equals("yellow"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.yellow;
        }
        }

        if(E0color.Equals("blue"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.blue;
        }
        }

        if(E0color.Equals("green"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.green;
        }
        }

        }

        if(go.name.Equals("Enemy_1(Clone)"))
        {

        if(E1color.Equals("white"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.white;
        }
        }

        if(E1color.Equals("yellow"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.yellow;
        }
        }

        if(E1color.Equals("blue"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.blue;
        }
        }

        if(E1color.Equals("green"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.green;
        }
        }

        }

        if(go.name.Equals("Enemy_2(Clone)"))
        {

        if(E2color.Equals("white"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.white;
        }
        }

        if(E2color.Equals("yellow"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.yellow;
        }
        }

        if(E2color.Equals("blue"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.blue;
        }
        }

        if(E2color.Equals("green"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.green;
        }
        }

        }

        if(go.name.Equals("Enemy_3(Clone)"))
        {

        if(E3color.Equals("white"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.white;
        }
        }

        if(E3color.Equals("yellow"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.yellow;
        }
        }

        if(E3color.Equals("blue"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.blue;
        }
        }

        if(E3color.Equals("green"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.green;
        }
        }

        }

        if(go.name.Equals("Enemy_4(Clone)"))
        {

        if(E4color.Equals("white"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.white;
        }
        }

        if(E4color.Equals("yellow"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.yellow;
        }
        }

        if(E4color.Equals("blue"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.blue;
        }
        }

        if(E4color.Equals("green"))
        {
        materials = Utils.GetAllMaterials(go);
        foreach(Material m in materials)
        {
            m.color = Color.green;
        }
        }

        }
    }

    public static void SaveGameData()
    {
        gameDataShooter newData = new gameDataShooter();
        string path = Application.streamingAssetsPath + "/SpaceLogs.json";
        foreach (GameLog log in Spacelogs)
        {
            newData.Add(log);
        }
        File.WriteAllText(path, JsonUtility.ToJson(newData));
    }
}

[System.Serializable]
public class gameDataShooter // create an object for Space shooter game data
{
    public List<GameLog> SpaceGame = new List<GameLog>();

    public void Add(GameLog log)
    {
        SpaceGame.Add(log);
    }
}