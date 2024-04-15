using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections;
using TMPro;

public class FlowerMiniGame : MonoBehaviour
{
    // draggable flowers
    public GameObject flowerItem1;
    public GameObject flowerItem2;
    public GameObject flowerItem3;

    // slots the flowers start in
    public GameObject startSlot1;
    public GameObject startSlot2;
    public GameObject startSlot3;

    // slots that check the flowers
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

    // flowers that show up at start
    public GameObject flower1;
    public GameObject flower2;
    public GameObject flower3;

    // timers
    public TMP_Text startTimer;
    public GameObject startTimerObject;

    // for changing the colors of the flowers that show up at start
    public Image flower1Image;
    public Image flower2Image;
    public Image flower3Image;

    // minigame panel(s)
    public GameObject flowerMinigame;
    public GameObject startPanel;

    // outside scripts
    public PlayerHealth playerHealth;
    public MissionControl missionControl;

    // audio sources
    public AudioSource correct;
    public AudioSource wrong;

    private List<Color> originalColors = new List<Color>()
    {
        Color.red, Color.green, Color.blue
    };

    private List<Color> workingColors = new List<Color>();
    private List<Color> playerList = new List<Color>();

    // coroutines
    public Coroutine startCoroutine;

    private void OnEnable()
    {
        missionControl.fountainPaused = true;
        missionControl.pipesPaused = true;

        // reset all variables
        workingColors.AddRange(originalColors);
        //colorList.AddRange(workingColors);

        // Set start panel active
        startPanel.SetActive(true);
        flower1.SetActive(false);
        flower2.SetActive(false);
        flower3.SetActive(false);
        startTimerObject.SetActive(false);

        // show flower order briefly (like 3 seconds)
        startCoroutine = StartCoroutine(StartMinigame());

        // after, start coroutine timer
    }

    IEnumerator StartMinigame()
    {
        Shuffle(workingColors);

        // set colors
        flower1Image.color = workingColors[0];
        flower2Image.color = workingColors[1];
        flower3Image.color = workingColors[2];

        yield return new WaitForSeconds(1f);

        flower1.SetActive(true);
        startTimer.text = "3";
        startTimerObject.SetActive(true);
        

        yield return new WaitForSeconds(1f);

        flower2.SetActive(true);
        startTimer.text = "2";

        yield return new WaitForSeconds(1f);

        flower3.SetActive(true);
        startTimer.text = "1";

        yield return new WaitForSeconds(1f);

        startPanel.SetActive(false);

        yield break;
    }

    // check if positions are correct (win/lose condition)
    public void Confirm()
    {
        //List<Color> playerList = new List<Color>{};

        playerList.Add(slot1.GetComponent<SlotCheck>().CheckSlotContents());
            if (slot1.GetComponent<SlotCheck>().CheckSlotContents() == Color.red)
            {
                Debug.Log("<color=red>This COLOR</color>");
            }
            else if (slot1.GetComponent<SlotCheck>().CheckSlotContents() == Color.green)
            {
                Debug.Log("<color=green>This COLOR</color>");
            }
            else if (slot1.GetComponent<SlotCheck>().CheckSlotContents() == Color.blue)
            {
                Debug.Log("<color=blue>This COLOR</color>");
            }
        playerList.Add(slot2.GetComponent<SlotCheck>().CheckSlotContents());
            if (slot2.GetComponent<SlotCheck>().CheckSlotContents() == Color.red)
            {
                Debug.Log("<color=red>This COLOR</color>");
            }
            else if (slot2.GetComponent<SlotCheck>().CheckSlotContents() == Color.green)
            {
                Debug.Log("<color=green>This COLOR</color>");
            }
            else if (slot2.GetComponent<SlotCheck>().CheckSlotContents() == Color.blue)
            {
                Debug.Log("<color=blue>This COLOR</color>");
            }
        playerList.Add(slot3.GetComponent<SlotCheck>().CheckSlotContents());
            if (slot3.GetComponent<SlotCheck>().CheckSlotContents() == Color.red)
            {
                Debug.Log("<color=red>This COLOR</color>");
            }
            else if (slot3.GetComponent<SlotCheck>().CheckSlotContents() == Color.green)
            {
                Debug.Log("<color=green>This COLOR</color>");
            }
            else if (slot3.GetComponent<SlotCheck>().CheckSlotContents() == Color.blue)
            {
                Debug.Log("<color=blue>This COLOR</color>");
            }

        CompareLists();
    }

    private bool CompareLists()
    {
        for (int i = 0; i < workingColors.Count; i++)
        {
            if (playerList[i] != workingColors[i])
            {
                playerHealth.healthCount -= 1;
                wrong.Play();
                EndMinigame();
                return false; // Player input does not match the game sequence = game over
            }
        }
        Debug.Log("Player successfully replicated the sequence!");

        // talk to mission control
        missionControl.flowerIsOkay = true;
        missionControl.fountainPaused = false;
        missionControl.pipesPaused = false;

        correct.Play();
        EndMinigame();
        return true; // Player input matches the game sequence
    }

    private void Shuffle(List<Color> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            // grab one of the strings in the list
            int random = Random.Range(i, list.Count);
            Color temporary = list[i];

            // move int to random number in list
            list[i] = list[random];
            list[random] = temporary;
        }
    }

    // close minigame & set flowerIsOkay to true (if correct)
    private void EndMinigame()
    {
        //GetComponent<MissionControl>().flowerIsOkay = true;
        flowerMinigame.SetActive(false);

        // set starting colors of flowers to white
        flower1Image.color = Color.white;
        flower2Image.color = Color.white;
        flower3Image.color = Color.white;

        // set draggable flowers to original positions
        flowerItem1.transform.SetParent(startSlot1.transform);
        flowerItem2.transform.SetParent(startSlot2.transform);
        flowerItem3.transform.SetParent(startSlot3.transform);

        // clear lists
        workingColors.Clear();
        playerList.Clear();
    }
}
