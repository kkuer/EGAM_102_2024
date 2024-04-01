using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    public static MissionControl Instance;
    
    public bool flowerIsOkay;
    public bool fountainIsOkay;

    public GameObject flowerButton;
    public GameObject fountainButton;

    public bool canForgeSignatures;
    public GameObject giantX;

    public Coroutine flowerSetActiveCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        Time.timeScale = 1;

        // set minigames active
        fountainIsOkay = true; // reminder to turn this to false when minigame is done
        SetFlowerActive();

        giantX.SetActive(true);
        canForgeSignatures = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (flowerIsOkay == true && fountainIsOkay == true)
        //{
        //    giantX.SetActive(false);
        //    canForgeSignatures = true;
        //}
        if (flowerIsOkay == false || fountainIsOkay == false)
        {
            giantX.SetActive(true);
            canForgeSignatures=false;
        }
        else
        {
            giantX.SetActive(false);
            canForgeSignatures = true;
        }

        if (flowerIsOkay == true)
        {
            flowerButton.SetActive(false);
            flowerSetActiveCoroutine = StartCoroutine(FlowerSetActiveCoroutine());
        }
    }

    IEnumerator FlowerSetActiveCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));
        SetFlowerActive();
        yield break;
    }

    // do a coroutine for the fountain

    public void SetFlowerActive()
    {
        flowerButton.SetActive(true);
        flowerIsOkay = false;
    }

    public void SetFountainActive()
    {
        fountainButton.SetActive(true);
        fountainIsOkay = false;
    }
}
