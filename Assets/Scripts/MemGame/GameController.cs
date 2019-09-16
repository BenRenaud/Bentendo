using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite CardImage;
    public Sprite[] puzzleSprites;
    public List<Sprite> gamePuzzles = new List<Sprite>();
    public List<Button> puzzleButtons = new List<Button>();
    private bool firstGuess, secondGuess;
    private int correctGuesses;
    private int gameGuesses;
    static public int score;
    static public float time;
    private string firstPuzzleButton, secondPuzzleButton;
    private int firstGuessIndex, secondGuessIndex;
    public Button RestartButton;
    public Text Score;
    public Text TimeLabel;
    public AudioSource MatchMade, Click;
    public AudioSource MatchNotMade;

    void Awake()
    {
        puzzleSprites = Resources.LoadAll<Sprite>("Sprites/Characters/");
        score = 1000;
        time = 0;
    }
    void Start()
    {
        GetButtons();
        AddListener();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
    }

    void Update()
    {
        time = time + Time.deltaTime;
        Score.text = "Score = " + score;
        TimeLabel.text = "Time Elapsed: " + Mathf.Round(time * 1f) / 1f + "s";

        if(score <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    void GetButtons()
    {
        GameObject[] Gobjects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for(int i = 0; i < Gobjects.Length; i++)
        {
            puzzleButtons.Add(Gobjects[i].GetComponent<Button>());
            puzzleButtons[i].image.sprite = CardImage;
        }

    }

    void AddGamePuzzles()
    {
        int NumofButtons = puzzleButtons.Count;
        int index = 0;

        for(int i = 0; i < NumofButtons; i++)
        {
            if(index == NumofButtons / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzleSprites[index]);
            index++;
        }

    }

    void AddListener()
    {
        foreach(Button PuzzleButton in puzzleButtons)
        {
            PuzzleButton.onClick.AddListener(() => PickAPuzzle());

        }
    }

    public void PickAPuzzle()
    {
        string GOname = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if(!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(GOname);
            firstPuzzleButton = gamePuzzles[firstGuessIndex].name;
            puzzleButtons[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if(!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(GOname);
            secondPuzzleButton = gamePuzzles[secondGuessIndex].name;
            puzzleButtons[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            StartCoroutine(CheckIfThePuzzleMatch());
        }

    }

    IEnumerator CheckIfThePuzzleMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstPuzzleButton == secondPuzzleButton){

            MatchMade.Play();
            yield return new WaitForSeconds(.5f);

            puzzleButtons[firstGuessIndex].interactable = false;
            puzzleButtons[secondGuessIndex].interactable = false;
            puzzleButtons[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            puzzleButtons[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();
        }

        else
        {
            MatchNotMade.Play();
            yield return new WaitForSeconds(.5f);

            score -= 40;
            puzzleButtons[firstGuessIndex].image.sprite = CardImage;
            puzzleButtons[secondGuessIndex].image.sprite = CardImage;
        }
        yield return new WaitForSeconds(.5f);
        firstGuess = secondGuess = false;
    }

    void CheckIfTheGameIsFinished()
    {
        correctGuesses++;

        if(correctGuesses == gameGuesses)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void Restart()
    {
        Click.Play();
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Click.Play();
        SceneManager.LoadScene("MemMainMenu");
    }
}