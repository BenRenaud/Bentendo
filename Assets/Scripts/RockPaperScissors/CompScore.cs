using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompScore : MonoBehaviour
{
public static int p2score = 0;
public Text CompsScore;

    public void Start()
    {
    CompsScore = GetComponent<Text> ();
    }
    public void Update()
    {
    CompsScore.text = "Computer Wins: " + p2score;   
    }
}