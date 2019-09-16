using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
public static int p1score = 0;
public Text PlayersScore;
    
    public void Start()
    {
    PlayersScore = GetComponent<Text> ();
    }
    public void Update()
    {
    PlayersScore.text = "Player Wins: " + p1score;
    }
}
