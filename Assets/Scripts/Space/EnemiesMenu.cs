using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemiesMenu : MonoBehaviour
{
    private int[] points = {25,50,75,100};
    private string[] colors = {"white","yellow","blue","green"};
    public Button ExitButton;
    public Dropdown E0P, E1P, E2P, E3P, E4P;
    public Dropdown E0C, E1C, E2C, E3C, E4C;
    public AudioSource click;

    public void Enemy0P(int num)
    {
        Main.E0Score = points[num];
    }

    public void Enemy1P(int num)
    {
        Main.E1Score = points[num];
    }

    public void Enemy2P(int num)
    {
        Main.E2Score = points[num];
    }

    public void Enemy3P(int num)
    {
        Main.E3Score = points[num];
    }

    public void Enemy4P(int num)
    {
        Main.E4Score = points[num];
    }

    public void Enemy0C(int num)
    {
        Main.E0color = colors[num];
    }

    public void Enemy1C(int num)
    {
        Main.E1color = colors[num];
    }

    public void Enemy2C(int num)
    {
        Main.E2color = colors[num];
    }

    public void Enemy3C(int num)
    {
        Main.E3color = colors[num];
    }

    public void Enemy4C(int num)
    {
        Main.E4color = colors[num];
    }
    public void Quit()
    {
        click.Play();
        SceneManager.LoadScene("SettingsMenu");
    }
}