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

    public int healthCount;

    public ShakeBehaviour shake;

    public bool powerupReady = true;

    // Start is called before the first frame update
    void Start()
    {
        healthCount = 3;
        shake = Camera.main.GetComponent<ShakeBehaviour>();
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
            Time.timeScale = 0;
            shake.shakeDuration = 0;

        }
    }

    public void applyPowerupHex()
    {
        powerupReady = false;
        //handle powerup logic
        powerupReady = true;
    }
    public void applyPowerupEnemy()
    {
        powerupReady = false;
        if (ThingSpawner.thingSpawnerInstance != null)
        {
            foreach (GameObject triangle in ThingSpawner.thingSpawnerInstance.activeTriangles)
            {
                Destroy(triangle);
            }
        }
        powerupReady = true;
    }
}
