using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource Musicplayer, Click;
    public GameObject Border, CopyText, CopyBG, CreatedByText, MainText, LogoutButton;
    public static GameObject OpenPannel, OpenPannel2, UserAccountPan, ConfigurationPan, HistoryPan, SpaceConfigPan, GamelevelsPan, StartGame;
    public GameObject CreateUserPan, DeleteUserPan, ChangePassPan, UnblockPan, AudioPan, DisplayPan, SpaceAudioPan, BronzePan, SilverPan, GoldPan, AppleHistoryPan, RPSHistoryPan, MemHistoryPan, SpaceDisplayPan, SpaceEnemiesPan, SpaceHistoryPan, SpaceErrorPan;
    public static AudioClip CasinoPark, LuigisCasino, MonkeyBill, StarMaze, SpaceLand, SpaceMusic, BowersCastle, Revenge, BrambleBlast, Win, Win2, Win3, Boom, Boom2, Boom3, Pew, Pew2, Pew3;
    public static Image image1, image2, image3, image4, image5, image6, image7, image8;
    public InputField CreateUserField, ChangePasswordField;
    public Text NewUserStatus, DeleteUserStatus, UnblockUserStatus, ChangePasswordStatus, WelcomeText, TopText, TopTextApple, TopTextRPS, TopTextMem, TopTextSpace;
    private string Username;
    public Dropdown UserList, BlockedList, HistoryDropdown, AppleHistoryDropdown, RPSHistoryDropdown, MemHistoryDropdown, SpaceHistoryDropdown;
    public Image preview, SpacePreview, Backroundimage;
    public Button CreateUserMenuButton, DeleteUserMenuButton, UnBlockUserMenuButton, SpaceLevelsExitButton;
    public Slider BGMusicSlider;
    public Image[] BGPics = {image1, image2, image3, image4, image5};
    public AudioClip[] MusicBG = {CasinoPark, LuigisCasino, MonkeyBill, Revenge, BrambleBlast};
    public AudioClip[] SpaceMusicBG = {StarMaze, SpaceLand, SpaceMusic, BowersCastle};
    public GameObject[] FileItems = {UserAccountPan, ConfigurationPan, HistoryPan};
    public GameObject[] SpaceFileItems = {StartGame, GamelevelsPan, ConfigurationPan, HistoryPan};
    public Image[] Spacepics = {image6, image7, image8};
    public AudioClip[] SpaceWinBG = {Win, Win2, Win3};
    public AudioClip[] SpacePewBG = {Pew, Pew2, Pew3};
    public AudioClip[] SpaceBoomBG = {Boom, Boom2, Boom3};
    public static List<GameLog> Applelogs = new List<GameLog>();
    public static List<GameLog> RPSlogs = new List<GameLog>();
    public static List<GameLog> Memlogs = new List<GameLog>();
    public static List<GameLog> Spacelogs = new List<GameLog>();

    void Awake()
    {
        WelcomeText.text = "Welcome " + Users.CurrentUser.username + " !";
        Border.SetActive(false);
        BGMusicSlider.value = Musicplayer.volume;
        PlayerPrefs.SetFloat("SpaceBGV", 1);
        PlayerPrefs.SetFloat("SpaceBGWinV", 1);
        PlayerPrefs.SetFloat("SpaceBGBoomV", 1);
        PlayerPrefs.SetFloat("SpaceBGPewV", 1);
        PlayerPrefs.SetInt("index", 0);
        PlayerPrefs.SetInt("indexSilver", 0);
        PlayerPrefs.SetInt("indexGold", 0);
        PlayerPrefs.SetFloat("X", 107);
        PlayerPrefs.SetFloat("Y", 80);
    }

    void Start()
    {
        string path = Application.streamingAssetsPath + "/AppleLogs.json"; // path to apple
        string jsonString = File.ReadAllText(path); // read all text from json file
        gameData data = JsonUtility.FromJson<gameData>(jsonString); // store the json data into a gamedata variable
        Applelogs.Clear();
        if (!(data == null)) // as long as there is more data
        {
            foreach (GameLog log in data.ApplePicker) // for each log in the data
            {
                Applelogs.Add(log); // add the log to Applelogs
            }
        }

        // This of these code segment are very similar for every game^^

        string RPSpath = Application.streamingAssetsPath + "/RPSLogs.json";
        string RPSjsonString = File.ReadAllText(RPSpath);
        gameDataRPS RPSdata = JsonUtility.FromJson<gameDataRPS>(RPSjsonString);
        RPSlogs.Clear();
        if (!(RPSdata == null))
        {
            foreach (GameLog log in RPSdata.RockPaperScissors)
            {
                RPSlogs.Add(log);
            }
        }

        string Mempath = Application.streamingAssetsPath + "/MemLogs.json";
        string MemjsonString = File.ReadAllText(Mempath);
        gameDataMemory Memdata = JsonUtility.FromJson<gameDataMemory>(MemjsonString);
        Memlogs.Clear();
        if (!(Memdata == null))
        {
            foreach (GameLog log in Memdata.MemoryGame)
            {
                Memlogs.Add(log);
            }
        }

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

    public void FileDropLogic(int num)
    {
        Click.Play();

        if(num == 0 && Users.CurrentUser.username != "admin") // if you are not the admin accessing user accounts
        {
            CreateUserMenuButton.interactable = false;
            DeleteUserMenuButton.interactable = false;
            UnBlockUserMenuButton.interactable = false;
        }

        if(num == 3) // if you are exiting the game
        {
            Users.CurrentUser.Logins.Add(new SessionLogObject());
            Users.DumpUsers();
            Application.Quit();
            PlayerPrefs.DeleteAll();
            
        }

        if(num == 2) // if you click on the history item
        {
            HistoryDropdown.ClearOptions();

            if(Users.CurrentUser.username != "admin") // if you are not the admin
            {
                LoadHistoryDropdown(); // load the non-admin history
            }

            else // if you are the admin
            {
                LoadHistoryDropdownAdmin(); // load the admin history
            }
        }
        OpenPannel = FileItems[num];
        OpenPannel.SetActive(true);
        Border.SetActive(true);
        CopyText.SetActive(false);
        CreatedByText.SetActive(false);
        MainText.SetActive(false);
        LogoutButton.SetActive(false);
        CopyBG.SetActive(false);
    }

    public void ExitFilePannel()
    {
        Click.Play();
        Border.SetActive(false);
        OpenPannel.SetActive(false);
        CopyText.SetActive(true);
        CreatedByText.SetActive(true);
        MainText.SetActive(true);
        LogoutButton.SetActive(true);
        CopyBG.SetActive(true);
    }

    public void LoadHistoryDropdown() // load history for non-admin
    {
        string header = string.Format("{0,-20}{1,-30}{2,-20}", "Username", "Date", "Length"); // header for dropdown
        TopText.text = header;

        foreach (SessionLogObject session in Users.CurrentUser.Logins) // for each session in Logins
        {
            string currentLog = string.Format("{0,-20}{1,-30}{2,-20}", Users.CurrentUser.username, session.Time, session.Length); // layout for current log
            HistoryDropdown.options.Add(new Dropdown.OptionData() { text = currentLog }); // add it to the dropdown
        }

    }

    public void LoadHistoryDropdownAdmin() // load history for admin
    {
        string header = string.Format("{0,-20}{1,-30}{2,-20}{3,-20}", "Username", "Date", "Length", "Status");
        TopText.text = header;

        foreach (User user in Users.GetUsers().Values) // for every user in the system
        {
            foreach (SessionLogObject session in user.Logins)
            {
                string currentLog = string.Format("{0,-20}{1,-30}{2,-20}{3,-20}", user.username, session.Time, session.Length, user.status);
                HistoryDropdown.options.Add(new Dropdown.OptionData() { text = currentLog });
            }
        }
    }

    public void CreateuserButton()
    {
        Click.Play();
        OpenPannel.SetActive(false);
        CreateUserPan.SetActive(true);
    }

    public void ExitCreateUser()
    {
        Click.Play();
        NewUserStatus.text = "";
        CreateUserPan.SetActive(false);
        OpenPannel.SetActive(true);
    }

    public void CreateUser() // create user in system
    {
        Click.Play();

        if (Users.ContainsUser(CreateUserField.text)) // if the user is already in system
        {
            NewUserStatus.color = Color.red;
            NewUserStatus.text = "User already exists.";
        } 
        
        else if (CreateUserField.text == "") // if input is empty
        {
            NewUserStatus.color = Color.red;
            NewUserStatus.text = "Username must consist of at least one character.";
        } 
        
        else // if everything went well
        {
            Users.AddUser(CreateUserField.text);
            NewUserStatus.color = Color.green;
            NewUserStatus.text = "User was created Successfully!";
        }
        
    }

    public void DeleteuserButton()
    {
        Click.Play();
        UserList.options.Clear();
        foreach (User user in Users.GetUsers().Values) {
            UserList.options.Add(new Dropdown.OptionData() { text = user.username });
        }
        OpenPannel.SetActive(false);
        DeleteUserPan.SetActive(true);
    }

    public void ExitDeleteUser()
    {
        Click.Play();
        DeleteUserStatus.text = "";
        DeleteUserPan.SetActive(false);
        OpenPannel.SetActive(true);
    }

    public void DeleteUser() // delete user from system
    {
        Click.Play();
        string selectedUser = UserList.options[UserList.value].text;

        if (selectedUser == "admin")
        {
            DeleteUserStatus.color = Color.red;
            DeleteUserStatus.text = "Cannot delete admin user!";
        } 
        
        else
        {
            Users.DeleteUser(selectedUser);
            DeleteUserStatus.color = Color.green;
            DeleteUserStatus.text = "Deleted user.";
            UserList.options.Remove(UserList.options[UserList.value]);
            UserList.value = 0;
        }
        
    }

    public void UnblockButton()
    {
        Click.Play();
        BlockedList.options.Clear();
        foreach (User user in Users.GetUsers().Values)
        {
            if (user.status == "blocked")
            {
                BlockedList.options.Add(new Dropdown.OptionData() { text = user.username });
            }
        }
        OpenPannel.SetActive(false);
        UnblockPan.SetActive(true);
    }

    public void ExitUnblock()
    {
        Click.Play();
        UnblockUserStatus.text = "";
        UnblockPan.SetActive(false);
        OpenPannel.SetActive(true);
    }

    public void UnblockUser() //unblock user in system
    {
        Click.Play();
        Users.UnblockUser(BlockedList.options[BlockedList.value].text);
        UnblockUserStatus.color = Color.green;
        UnblockUserStatus.text = "Unblocked user!";
        BlockedList.options.Remove(BlockedList.options[BlockedList.value]);
        BlockedList.value = 0;
    }

    public void ChangePassButton()
    {
        Click.Play();
        OpenPannel.SetActive(false);
        ChangePassPan.SetActive(true);
    }

    public void ExitChangePass()
    {
        Click.Play();
        ChangePasswordStatus.text = "";
        ChangePassPan.SetActive(false);
        OpenPannel.SetActive(true);
    }

    public void ChangePassword() // change the current users password
    {
        
        Click.Play();
        if(Users.CurrentUser.password == ChangePasswordField.text) // if you try to use the same password
        {
            ChangePasswordStatus.color = Color.red;
            ChangePasswordStatus.text = "Password must be different from your previous password.";
        }

        else if(Users.CurrentUser.username == "admin" && ChangePasswordField.text != "") // if you are the admin
        {
            ChangePasswordStatus.color = Color.green;
            ChangePasswordStatus.text = "admin password has been changed!";
            Users.ChangePassword(ChangePasswordField.text);
        }

        else if (ChangePasswordField.text != "" && Users.CurrentUser.username != "admin") // if you arnt the admin
        {
            Users.ChangePassword(ChangePasswordField.text);
            ChangePasswordStatus.color = Color.green;
            ChangePasswordStatus.text = "Successfully changed password!";
        }

        else // if you dont put anything in the input field
        {
            ChangePasswordStatus.color = Color.red;
            ChangePasswordStatus.text = "Password must contain at least one character.";
        }
        
    }

    public void AudioButton()
    {
        Click.Play();
        OpenPannel.SetActive(false);
        AudioPan.SetActive(true);
    }

    public void ExitAudio()
    {
        Click.Play();
        AudioPan.SetActive(false);
        OpenPannel.SetActive(true);
    }

    public void setBGV(float value)
    {
        Musicplayer.volume = value; // set the volume of the BG music
    }

    public void BackroundMusic(int num)
    {
        Musicplayer.Stop();
        Musicplayer.clip = MusicBG[num]; // set the BG music
        Musicplayer.Play();
    }

    public void DisplayButton()
    {
        Click.Play();
        OpenPannel.SetActive(false);
        DisplayPan.SetActive(true);
    }

    public void ExitDisplay()
    {
        Click.Play();
        Backroundimage.sprite = preview.sprite; // change the BG on exit
        DisplayPan.SetActive(false);
        OpenPannel.SetActive(true);
    }

    public void BackroundM(int num)
    {
        preview.sprite = BGPics[num].sprite; // show preview of BG
    }

    public void AppleDropLogic(int num)
    {
        Click.Play();

        if(num == 1) // show Apple picker history
        {
            LoadHistoryDropdownApple();
            AppleHistoryPan.SetActive(true);
            Border.SetActive(true);
            CopyText.SetActive(false);
            CreatedByText.SetActive(false);
            MainText.SetActive(false);
            LogoutButton.SetActive(false);
            CopyBG.SetActive(false);
        }

        else // start game
        {
            SceneManager.LoadScene("ApplePickerGame");
        }
    }

    public void ExitAppleHistory()
    {
        Click.Play();
        Border.SetActive(false);
        AppleHistoryPan.SetActive(false);
        CopyText.SetActive(true);
        CreatedByText.SetActive(true);
        MainText.SetActive(true);
        LogoutButton.SetActive(true);
        CopyBG.SetActive(true);
    }

    public void LoadHistoryDropdownApple() // load the apple picker history
    {
        AppleHistoryDropdown.ClearOptions();
        string header = string.Format("{0,-20}{1,-30}{2,-20}", "Username", "Date", "Score");
        TopTextApple.text = header;
        
        foreach (GameLog log in Applelogs)
        {
            string currentLog = string.Format("{0,-20}{1,-30}{2,-20}", log.Username, log.Date, log.Score);
            AppleHistoryDropdown.options.Add(new Dropdown.OptionData() { text = currentLog });
        }
    }

    public void RPSDropLogic(int num)
    {
        Click.Play();

        if(num == 1) // show rock paper scissors history
        {
            LoadHistoryDropdownRPS();
            RPSHistoryPan.SetActive(true);
            Border.SetActive(true);
            CopyText.SetActive(false);
            CreatedByText.SetActive(false);
            MainText.SetActive(false);
            LogoutButton.SetActive(false);
            CopyBG.SetActive(false);
        }

        else // start game
        {
            SceneManager.LoadScene("SinglePlayer");
        }
    }

    public void ExitRPSHistory()
    {
        Click.Play();
        Border.SetActive(false);
        RPSHistoryPan.SetActive(false);
        CopyText.SetActive(true);
        CreatedByText.SetActive(true);
        MainText.SetActive(true);
        LogoutButton.SetActive(true);
        CopyBG.SetActive(true);
    }

    public void LoadHistoryDropdownRPS() // load history for Rock paper scissors
    {
        RPSHistoryDropdown.ClearOptions();
        string header = string.Format("{0,-20}{1,-30}{2,-20}", "Username", "Date", "Winner");
        TopTextRPS.text = header;
        
        foreach (GameLog log in RPSlogs)
        {
            string currentLog = string.Format("{0,-20}{1,-30}{2,-20}", log.Username, log.Date, log.Score);
            RPSHistoryDropdown.options.Add(new Dropdown.OptionData() { text = currentLog });
        }
    }

    public void MemDropLogic(int num)
    {
        Click.Play();

        if(num == 1) // show memory game history
        {
            LoadHistoryDropdownMem();
            MemHistoryPan.SetActive(true);
            Border.SetActive(true);
            CopyText.SetActive(false);
            CreatedByText.SetActive(false);
            MainText.SetActive(false);
            LogoutButton.SetActive(false);
            CopyBG.SetActive(false);
        }

        else // start game
        {
            SceneManager.LoadScene("MemMainMenu");
        }
    }

    public void LoadHistoryDropdownMem() // load memory game history
    {
        MemHistoryDropdown.ClearOptions();
        string header = string.Format("{0,-20}{1,-30}{2,-20}", "Username", "Date", "Score");
        TopTextMem.text = header;
        
        foreach (GameLog log in Memlogs)
        {
            string currentLog = string.Format("{0,-20}{1,-30}{2,-20}", log.Username, log.Date, log.Score);
            MemHistoryDropdown.options.Add(new Dropdown.OptionData() { text = currentLog });
        }
    }

    public void ExitMemHistory()
    {
        Click.Play();
        Border.SetActive(false);
        MemHistoryPan.SetActive(false);
        CopyText.SetActive(true);
        CreatedByText.SetActive(true);
        MainText.SetActive(true);
        LogoutButton.SetActive(true);
        CopyBG.SetActive(true);
    }

    public void SpaceDropLogic(int num)
    {
        Click.Play();

        if(num == 0) // start game
        {
            SceneManager.LoadScene("_Scene_0");
        }

        if(num == 3) // show space shooter history
        {
            SpaceHistoryDropdown.ClearOptions();
            LoadSpaceHistoryDropdown();
            SpaceHistoryPan.SetActive(true);
            Border.SetActive(true);
            CopyText.SetActive(false);
            CreatedByText.SetActive(false);
            MainText.SetActive(false);
            LogoutButton.SetActive(false);
            CopyBG.SetActive(false);
        }

        else // open configurations or game levels
        {
            OpenPannel2 = SpaceFileItems[num];
            OpenPannel2.SetActive(true);
            Border.SetActive(true);
            CopyText.SetActive(false);
            CreatedByText.SetActive(false);
            MainText.SetActive(false);
            LogoutButton.SetActive(false);
            CopyBG.SetActive(false);
        }
    }

    public void ExitSpacePannel()
    {
        Click.Play();
        Border.SetActive(false);
        OpenPannel2.SetActive(false);
        CopyText.SetActive(true);
        CreatedByText.SetActive(true);
        MainText.SetActive(true);
        LogoutButton.SetActive(true);
        CopyBG.SetActive(true);
    }

    public void ExitSpaceGameLevelsPannel()
    {
        int NumofEisGood = 0;
        int ScoreisGood = 0;

        if(BronzeLevel.NumofE < SilverLevel.NumofESilver && SilverLevel.NumofESilver < GoldLevel.NumofEGold)
        {
            NumofEisGood = 1;
        }

        else
        {
            NumofEisGood = 0;
        }

        if(BronzeLevel.ScoreToWin < SilverLevel.ScoreToWinSilver && SilverLevel.ScoreToWinSilver < GoldLevel.ScoreToWinGold)
        {
            ScoreisGood = 1;
        }

        else
        {
            ScoreisGood = 0;
        }

        if(ScoreisGood == 1 && NumofEisGood == 1)
        {
            Click.Play();
            Border.SetActive(false);
            OpenPannel2.SetActive(false);
            CopyText.SetActive(true);
            CreatedByText.SetActive(true);
            MainText.SetActive(true);
            LogoutButton.SetActive(true);
            CopyBG.SetActive(true);
        }

        else
        {
            SpaceErrorPan.SetActive(true);
            SpaceLevelsExitButton.interactable = false;
        }
    }

    public void ExitErrorSpace()
    {
        SpaceErrorPan.SetActive(false);
        SpaceLevelsExitButton.interactable = true;
    }

    public void BronzeButton()
    {
        Click.Play();
        OpenPannel2.SetActive(false);
        BronzePan.SetActive(true);
    }

    public void ExitBronze()
    {
        Click.Play();
        OpenPannel2.SetActive(true);
        BronzePan.SetActive(false);
    }

     public void SilverButton()
    {
        Click.Play();
        OpenPannel2.SetActive(false);
        SilverPan.SetActive(true);
    }

    public void ExitSilver()
    {
        Click.Play();
        OpenPannel2.SetActive(true);
        SilverPan.SetActive(false);
    }

    public void GoldButton()
    {
        Click.Play();
        OpenPannel2.SetActive(false);
        GoldPan.SetActive(true);
    }

    public void ExitGold()
    {
        Click.Play();
        OpenPannel2.SetActive(true);
        GoldPan.SetActive(false);
    }

    public void SpacesetBGV(float value)
    {
        PlayerPrefs.SetFloat("SpaceBGV", value);
    }

    public void SpacesetBGWinV(float value)
    {
        PlayerPrefs.SetFloat("SpaceBGWinV", value);
    }

    public void SpacesetBGBoomV(float value)
    {
        PlayerPrefs.SetFloat("SpaceBGBoomV", value);
    }

    public void SpacesetBGPewV(float value)
    {
        PlayerPrefs.SetFloat("SpaceBGPewV", value);
    }

    public void SpaceBackroundMusic(int num)
    {
        PlayerPrefs.SetInt("SpaceBG", num);
    }

    public void SpaceBackroundWin(int num)
    {
        PlayerPrefs.SetInt("SpaceBGWin", num);
    }

    public void SpaceBackroundBoom(int num)
    {
        PlayerPrefs.SetInt("SpaceBGBoom", num);
    }

    public void SpaceBackroundPew(int num)
    {
        PlayerPrefs.SetInt("SpaceBGPew", num);
    }

    public void SpaceAudioButton()
    {
        Click.Play();
        OpenPannel2.SetActive(false);
        SpaceAudioPan.SetActive(true);
    }

    public void ExitSpaceAudio()
    {
        Click.Play();
        SpaceAudioPan.SetActive(false);
        OpenPannel2.SetActive(true);
    }

    public void SpaceEnemiesButton()
    {
        Click.Play();
        OpenPannel2.SetActive(false);
        SpaceEnemiesPan.SetActive(true);
    }

    public void ExitEnemies()
    {
        Click.Play();
        SpaceEnemiesPan.SetActive(false);
        OpenPannel2.SetActive(true);
    }

    public void SpaceDisplayButton()
    {
        Click.Play();
        OpenPannel2.SetActive(false);
        SpaceDisplayPan.SetActive(true);
    }

    public void ExitSpaceDisplay()
    {
        Click.Play();
        SpaceDisplayPan.SetActive(false);
        OpenPannel2.SetActive(true);
    }

    public void SpaceBackroundM(int num)
    {
        SpacePreview.sprite = Spacepics[num].sprite;
        PlayerPrefs.SetInt("index", num);
    }

    public void SpaceBackroundMSilver(int num)
    {
        SpacePreview.sprite = Spacepics[num].sprite;
        PlayerPrefs.SetInt("indexSilver", num);
    }

    public void SpaceBackroundMGold(int num)
    {
        SpacePreview.sprite = Spacepics[num].sprite;
        PlayerPrefs.SetInt("indexGold", num);
    }

    public void LoadSpaceHistoryDropdown() // load the space shooter history
    {
        string header = string.Format("{0,-20}{1,-30}{2,-20}{3,-20}", "Username", "Date", "Score", "Level");
        TopTextSpace.text = header;

        foreach (GameLog log in Spacelogs)
        {
            string currentLog = string.Format("{0,-20}{1,-30}{2,-20}{3,-20}", log.Username, log.Date, log.Score, log.HighestLevel);
            SpaceHistoryDropdown.options.Add(new Dropdown.OptionData() { text = currentLog });
        }
    }

    public void ExitSpaceHistory()
    {
        Click.Play();
        Border.SetActive(false);
        SpaceHistoryPan.SetActive(false);
        CopyText.SetActive(true);
        CreatedByText.SetActive(true);
        MainText.SetActive(true);
        LogoutButton.SetActive(true);
        CopyBG.SetActive(true);
    }

    public void Logout() // logout to login screen and youre time spent is logged into the system
    {
        Users.CurrentUser.Logins.Add(new SessionLogObject());
        Users.DumpUsers();
        Click.Play();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("LoginScene");
    }
}