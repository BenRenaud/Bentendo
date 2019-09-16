using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Users : MonoBehaviour {

    public static Dictionary<string, User> users = new Dictionary<string, User>(); // string is the username and the value is a User object
    public static User currentUser = null;
    public static float currentUserStartTime; // time the user logged in

    void Start () {

        if (CurrentUser == null)
        {

            string path = Application.streamingAssetsPath + "/UserData.json"; // path to UserData json file
            string jsonString = File.ReadAllText(path); // read all text from json file
            userData data = JsonUtility.FromJson<userData>(jsonString); //put text into user data

            foreach (User user in data.Users) // for each user in data Users
            {
                users.Add(user.Username, user); // add the user into the system
            }
            DumpUsers(); // dump users into console and json file
        } 
    }

    public static Boolean ContainsUser(string name) // function for checking if something contains a certain user
    {
        return users.ContainsKey(name);
    }

    public static User GetUser(string name) // get the certain user using their username
    {
        return users[name];
    }

    public static void AddUser(string name) // add user to the system
    {
        users.Add(name, new User(name));
        DumpUsers();
    }

    public static void DeleteUser(string name) // delete user from the system
    {
        users.Remove(name);
        DumpUsers();
    }

    public static void UnblockUser(string name) // unblock a user from the system
    {
        users[name].status = "normal";
        DumpUsers();
    }

    public static void BlockUser(string name) // block a user from the system
    {
        users[name].status = "blocked";
        DumpUsers();
    }

    public static void ChangePassword(string newPassword) // change the password of a user in the system
    {
        if (CurrentUser != null)
        {
            users[CurrentUser.username].password = newPassword;
        }
        DumpUsers();
    }

    public static void DumpUsers() // dump users into the system(json file) and console
    {
        userData newUsers = new userData(); // new users
        string path = Application.streamingAssetsPath + "/UserData.json"; // path to system

        foreach (User user in users.Values)
        { 
            newUsers.Add(user); // add user to userData
        }
        Debug.Log(JsonUtility.ToJson(newUsers)); // show system on console
        File.WriteAllText(path, JsonUtility.ToJson(newUsers)); // add users to system
    }

    public static Dictionary<string,User> GetUsers() // get users from users dictionary
    {
        return users;
    }

    public static User CurrentUser // grab the current user logged in
    {
        get { return currentUser; }
        set { currentUser = value; }
    }
}

[System.Serializable]
public class SessionLogObject // create a object for login sessions
{
    public string Length; // how long they were logged in
    public string Time; // the time they log in
    public SessionLogObject()
    {
        this.Length = System.String.Format("{0:0.00}s", UnityEngine.Time.time - Users.currentUserStartTime);
        this.Time = System.DateTime.Now.ToString(); 
    }
}



[System.Serializable]
public class userData // create a object for user data
{
    public List<User> Users = new List<User>(); // list of users
    public void Add(User user)
    {
        Users.Add(user); // add user to users list
    }
}



[System.Serializable]
public class User // create a user object for login's and game history
{
    public string Username; // username of user
    public string Password; // password of user
    public int Score; // score of user
    public string Status; // statis of user
    public List<SessionLogObject> Logins = new List<SessionLogObject>(); // the list of login sessions for each user

    public User(string name) // default constructor
    {
        this.Username = name;
        this.Password = name;
        this.Score = 0;
        this.Status = "new";
    }

    public User(string name, string password, int score, string status) // overloaded constructor
    {
        this.Username = name;
        this.Password = password;
        this.Score = score;
        this.Status = status;
    }

    public string username // for getting username of current user
    {
        get { return this.Username; }
        set { this.Username = value; }
    }

    public string password // for getting password of current user
    {
        get { return this.Password; }
        set { this.Password = value; }
    }

    public string status // for getting status of current user
    {
        get { return this.Status; }
        set { this.Status = value; }
    }

    public int score // for getting score of current user
    {
        get { return this.Score; }
        set { this.Score = value; }
    }
}