using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FountainMiniGame : MonoBehaviour
{
    public int frogsLeft = 5;

    public GameObject bucket;

    public GameObject fountainMiniGame;

    public TMP_Text frogsLeftText;

    // outside scripts
    public PlayerHealth playerHealth;
    public MissionControl missionControl;

    // Set up frog spawner
    public List<Transform> spawnPoints = new List<Transform>();
    public List<DraggableItem> frogPrefabList;
    public int spawnCount = 5;
    public CanvasGroup canvasGroup;

    // timer stuff
    public float timeRemaining = 8;
    public bool timerIsRunning = false;
    public Slider sliderTimer;
    public TMP_Text timeText;

    // audio sources
    public AudioSource correct;
    public AudioSource wrong;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        timeRemaining = 8;
        missionControl.flowerPaused = true;
        missionControl.pipesPaused = true;

        timerIsRunning = true;
        sliderTimer.maxValue = timeRemaining;
        sliderTimer.value = timeRemaining;

        // reset all variables
        frogsLeft = 5;

        // instantiate the frogs
        Spawn();

        // start (countdown) timer
    }

    // Update is called once per frame
    void Update()
    {
        if (frogsLeft <= 0)
        {
            // talk to mission control
            missionControl.fountainIsOkay = true;
            missionControl.flowerPaused = false;
            missionControl.pipesPaused = false;

            // end minigame
            fountainMiniGame.SetActive(false);
            correct.Play();
            timerIsRunning = false;
        }

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
                fountainMiniGame.SetActive(false);
                playerHealth.healthCount--;
                wrong.Play();
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

    public void Spawn()
    {
        Debug.Log("spawner activated");
        List<Transform> usedPoints = new List<Transform>();

        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex = Random.Range(3, spawnPoints.Count);

            Transform spawnHandle = spawnPoints[randomIndex];

            while (usedPoints.Contains(spawnHandle))
            {
                randomIndex = Random.Range(0, spawnPoints.Count);
                spawnHandle = spawnPoints[randomIndex];
            }

            int randomShapeIndex = Random.Range(0, frogPrefabList.Count);
            DraggableItem randomPrefab = frogPrefabList[randomShapeIndex];

            // Instantiate prefab as child
            DraggableItem newShape = Instantiate(randomPrefab, spawnHandle);

            canvasGroup = newShape.canvasGroup;

            // move the prefab to the spawn position
            newShape.moveHandles.position = spawnHandle.position;

            usedPoints.Add(spawnHandle);
        }
    }

    // minigame completed sucessfully (set minigame inactive) & set fountainIsOkay to true



    // timer runs out (deal damage)
}
