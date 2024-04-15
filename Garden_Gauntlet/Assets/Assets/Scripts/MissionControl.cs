using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    public static MissionControl Instance;
    
    public bool flowerIsOkay;
    public bool fountainIsOkay;
    public bool pipesAreOkay;

    public GameObject flowerButton;
    public GameObject fountainButton;
    public GameObject pipesButton;

    public bool canForgeSignatures;
    public GameObject giantX;
    public GameObject typeText;

    public GameObject fireFlower;
    public GameObject fireFountain;
    public GameObject firePipes;

    public Coroutine flowerSetActiveCoroutine;
    public Coroutine fountainSetActiveCoroutine;
    public Coroutine pipesSetActiveCoroutine;

    public bool flowerPaused;
    public bool fountainPaused;
    public bool pipesPaused;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        Time.timeScale = 1;

        // set minigames active
        //fountainIsOkay = true; // reminder to turn this to false when minigame is done
        SetFlowerActive();

        giantX.SetActive(true);
        canForgeSignatures = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flowerIsOkay == false || fountainIsOkay == false || pipesAreOkay == false)
        {
            giantX.SetActive(true);
            typeText.SetActive(false);
            canForgeSignatures=false;
        }
        else
        {
            giantX.SetActive(false);
            typeText.SetActive(true);
            canForgeSignatures = true;
        }

        if (flowerIsOkay == true && flowerSetActiveCoroutine == null)
        {
            flowerButton.SetActive(false);
            fireFlower.SetActive(false);
            flowerSetActiveCoroutine = StartCoroutine(FlowerSetActiveCoroutine());
        }
        if (fountainIsOkay == true && fountainSetActiveCoroutine == null)
        {
            fountainButton.SetActive(false);
            fireFountain.SetActive(false);
            fountainSetActiveCoroutine = StartCoroutine(FountainSetActiveCoroutine());
        }
        if (pipesAreOkay == true && pipesSetActiveCoroutine == null)
        {
            pipesButton.SetActive(false);
            firePipes.SetActive(false);
            pipesSetActiveCoroutine = StartCoroutine(PipesSetActiveCoroutine());
        }
    }

    IEnumerator FlowerSetActiveCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));

        for (int i = 0; i < 5; i++)
        {
            while (flowerPaused)
            {
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2f);
        }

        SetFlowerActive();
        //yield break;
        flowerSetActiveCoroutine = null;
    }

    // do a coroutine for the fountain
    IEnumerator FountainSetActiveCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));

        for (int i = 0; i < 5; i++)
        {
            while (fountainPaused)
            {
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2f);
        }

        SetFountainActive();
        //yield break;
        fountainSetActiveCoroutine = null;
    }

    IEnumerator PipesSetActiveCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));

        for (int i = 0; i < 5; i++)
        {
            while (pipesPaused)
            {
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2f);
        }

        SetPipesActive();
        //yield break;
        pipesSetActiveCoroutine = null;
    }

    public void SetFlowerActive()
    {
        flowerButton.SetActive(true);
        fireFlower.SetActive(true);
        flowerIsOkay = false;
    }

    public void SetFountainActive()
    {
        fountainButton.SetActive(true);
        fireFountain.SetActive(true);
        fountainIsOkay = false;
    }

    public void SetPipesActive()
    {
        pipesButton.SetActive(true);
        firePipes.SetActive(true);
        pipesAreOkay = false;
    }
}
