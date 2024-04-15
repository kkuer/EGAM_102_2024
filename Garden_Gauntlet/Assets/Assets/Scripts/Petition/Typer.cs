using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public TMP_Text wordOutput = null;
    public TMP_Text namesTyped = null;

    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    public int nameCount = 0;

    public MissionControl missionControl;

    public GameObject winScreen;
    public GameObject restartButton;
    public GameObject panel;

    // (script referenced from youtube tutorial: https://www.youtube.com/watch?v=j98a_X9G1fM)

    void Start()
    {
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        // get word from word bank
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        // update displayed text
        wordOutput.text = remainingWord;
    }

    // Update is called once per frame
    void Update()
    {
        if (missionControl.canForgeSignatures == true)
        {
            CheckInput();
        }

        if (nameCount == 15)
        {
            winScreen.SetActive(true);
            restartButton.SetActive(true);
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            // check to make sure only one key is pressed that frame so it doesnt get confused
            if(keysPressed.Length == 1)
                EnterLetter(keysPressed);
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (IsWordComplete())
            {
                IncreasePetitionCount();
                SetCurrentWord();
            }   
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        // if index of letter is the correct one (0), then its the one we're looking for
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        // remove first letter of the word
        string newString = remainingWord.Remove(0,1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }

    private void IncreasePetitionCount()
    {
        nameCount++;
        namesTyped.text = nameCount.ToString();
    }
}
