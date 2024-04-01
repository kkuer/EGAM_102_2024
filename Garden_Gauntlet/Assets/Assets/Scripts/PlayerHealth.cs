using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthThree;
    public Image healthTwo;
    public Image healthOne;

    public GameObject gameOver;
    public GameObject restartButton;

    public int healthCount;

    // Start is called before the first frame update
    void Start()
    {
        healthCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCount == 2)
        {
            healthThree.enabled = false;
        }
        if (healthCount == 1)
        {
            healthTwo.enabled = false;
        }
        if (healthCount == 0)
        {
            healthOne.enabled = false;
            gameOver.SetActive(true);
            restartButton.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
