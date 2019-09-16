using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public static int NumOfCards = 12;

    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject puzzleButton;
    
    void Awake()
    {
        for(int j = 0; j < NumOfCards; j++)
        {
            GameObject button = Instantiate(puzzleButton);
            button.name = "" + j;
            button.transform.SetParent(puzzleField, false);
        }

    }
}
