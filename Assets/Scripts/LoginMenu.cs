using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginMenu : MonoBehaviour
{
    public Button LoginButton, ExitButton;
    public Text usernameField, passwordField, TryagainButton;
    public Text InvalidTextPrompt, NewPassword;
    private string Username, Password, CheckPassword;
    public AudioSource Click, LoginSound;
    public Image FadeImage;
    public GameObject WrongPassPan, TryagainImage, ErrorPan, ChangePasswordPan;
    public static Dictionary<string, int> FailedLogins = new Dictionary<string, int>();

    public void Login()
    {
        LoginSound.Play();
        if(Users.ContainsUser(usernameField.text)) // if the user is in the users list
        {
            User AttemptUserLogin = Users.GetUser(usernameField.text); // create a temporary user tester

            if(AttemptUserLogin.status == "blocked") // if this attempted user is blocked from the system
            {
                TryagainImage.SetActive(true);
                ErrorPan.SetActive(true);
                InvalidTextPrompt.text = "User is blocked from signing in.";
                TryagainButton.text = "Okay";
            }
           
            if(AttemptUserLogin.password == passwordField.text && AttemptUserLogin.status != "blocked") // if the password works and they are not blocked
            {
                Users.CurrentUser = Users.GetUser(usernameField.text); // attmpted user is valid and is set to current user
                Users.currentUserStartTime = Time.time; // the user has successfully logged in and save the time it was succsessful

                if(Users.CurrentUser.status == "new") // if the current user is new and has not made a password
                {
                    Users.CurrentUser.status = "normal";
                    TryagainImage.SetActive(true);
                    ChangePasswordPan.SetActive(true);
                }

                else // everything worked out perfectly
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }

        else // the password did not work but the user exists
        {
            if (usernameField.text != "admin") // you cannot ban the admin
            {
                int remainingAttempts = addFailedLogin(usernameField.text); // remaining attempts for user to try to login

                if (remainingAttempts == 0) // if they are all out of attempts
                {
                    TryagainImage.SetActive(true);
                    ErrorPan.SetActive(true);
                    InvalidTextPrompt.text = "Too many invalid login attempts, your account is now blocked!";
                    TryagainButton.text = "Okay";
                    Users.BlockUser(AttemptUserLogin.username);
                }

                else if(remainingAttempts == -1) // they try to log in evem though they are out of attempts
                {
                    TryagainImage.SetActive(true);
                    ErrorPan.SetActive(true);
                    InvalidTextPrompt.text = "Sorry you have been blocked, talk to the admin to unblock you.";
                    TryagainButton.text = "Okay";
                }

                else // you still have attempts
                {
                    TryagainImage.SetActive(true);
                    ErrorPan.SetActive(true);
                    InvalidTextPrompt.text = "That is an invalid password. This account will be blocked after " + remainingAttempts + " more invalid password(s)!";
                }
            } 
                
        else // the password was not correct
        {
            TryagainImage.SetActive(true);
            ErrorPan.SetActive(true);
            InvalidTextPrompt.text = "This is an invalid password.";
        }
        }
    }
        
    else // the user in not in the user list
    {
        TryagainImage.SetActive(true);
        ErrorPan.SetActive(true);
        InvalidTextPrompt.text = "This user does not exist.";
    }
    }

    public int addFailedLogin(string username) // decrease the failed log in attempts
    {
        User AttemptUserLogin = Users.GetUser(usernameField.text); // create a temporary user tester

        if(AttemptUserLogin.status == "blocked") // if this attempted user is blocked from the system
        {
            FailedLogins[username] = -1;
        }

        else if (FailedLogins.ContainsKey(username)) // decrease the log in attmepts
        {
            FailedLogins[username]--;
        } 
        
        else // set login attempts to 2
        {
            FailedLogins[username] = 2;
        }

        return FailedLogins[username]; // return how many attempts are left
    }

    public void TryagainButtonClick()
    {
        Click.Play();
        TryagainImage.SetActive(false);
        WrongPassPan.SetActive(false);
    }

    public void ChangePassButton()
    {
        Users.ChangePassword(NewPassword.text); // change password of current user
        WrongPassPan.SetActive(false);
        TryagainImage.SetActive(false);
        LoginSound.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Click.Play();
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
