using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PipeMiniGame : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    public int totalPipes = 0;

    public int correctedPipes = 0;

    // timer stuff
    public float timeRemaining = 8;
    public bool timerIsRunning = false;
    public Slider sliderTimer;
    public TMP_Text timeText;


    public PlayerHealth playerHealth;
    public MissionControl missionControl;

    public GameObject pipeMiniGame;

    // audio sources
    public AudioSource correct;
    public AudioSource wrong;

    public Coroutine coroutine;

    private void Start()
    {
        totalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }

        timeRemaining = 8;

        timerIsRunning = true;
        sliderTimer.maxValue = timeRemaining;
        sliderTimer.value = timeRemaining;

        Time.timeScale = 1;

        missionControl.fountainPaused = true;
        missionControl.flowerPaused = true;
    }

    private void OnEnable()
    {
        totalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }

        timeRemaining = 8;

        timerIsRunning = true;
        sliderTimer.maxValue = timeRemaining;
        sliderTimer.value = timeRemaining;

        Time.timeScale = 1;

        missionControl.fountainPaused = true;
        missionControl.flowerPaused = true;
    }

    public void CorrectMove()
    {
        correctedPipes++;

        if(correctedPipes == totalPipes)
        {
            coroutine = StartCoroutine(EndMinigame());
        }
    }

    IEnumerator EndMinigame()
    {
        yield return new WaitForSeconds(0.5f);

            Debug.Log("win!!");
            timerIsRunning=false;
            pipeMiniGame.SetActive(false);
            correct.Play();

            missionControl.fountainPaused = false;
            missionControl.flowerPaused = false;
            missionControl.pipesAreOkay = true;

        yield break;
    }

    public void WrongMove()
    {
        correctedPipes--;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            DisplayTime(timeRemaining);

            //timeText.text = timeRemaining.ToString();

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                sliderTimer.value = timeRemaining;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                pipeMiniGame.SetActive(false);
                playerHealth.healthCount--;
                wrong.Play();

                missionControl.fountainPaused = false;
                missionControl.flowerPaused = false;
                //Time.timeScale = 0;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float miliseconds = Mathf.FloorToInt(((timeToDisplay % 60) * 100.0f) % 100.0f);
        timeText.text = string.Format("{0:00}:{1:00}", seconds, miliseconds);
    }

    //public Puzzle puzzle;


    //[SerializeField]
    //public class Puzzle
    //{
    //    public int width;
    //    public int height;
    //    public PipePiece[,] pieces;
    //}



    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //private void OnEnable()
    //{
    //    Vector2 dimensions = CheckDimensions();

    //    puzzle.width =(int)dimensions.x;
    //    puzzle.height = (int)dimensions.y;

    //    puzzle.pieces = new PipePiece[puzzle.width, puzzle.height];

    //    foreach(var piece in GameObject.FindGameObjectsWithTag("Piece"))
    //    {
    //        puzzle.pieces[(int)piece.transform.position.y, (int)piece.transform.position.x] = piece.GetComponent<PipePiece>();
    //    }

    //    foreach (var piece in puzzle.pieces)
    //    {
    //        Debug.Log(piece.gameObject.name);
    //    }
    //}

    //Vector2 CheckDimensions()
    //{
    //    Vector2 aux = Vector2.zero;

    //    GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");

    //    foreach (var p in pieces)
    //    {
    //        if (p.transform.position.x > aux.x)
    //        {
    //            aux.x = p.transform.position.x;
    //        }
    //        if (p.transform.position.y > aux.y)
    //        {
    //            aux.y = p.transform.position.y;
    //        }
    //    }

    //    aux.x += 100;
    //    aux.y += 100;

    //    return aux; 
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
