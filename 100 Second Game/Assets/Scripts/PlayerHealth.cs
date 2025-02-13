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

    public GameObject damageParticlesPrefab;
    public GameObject explosionParticlesPrefab;
    public GameObject resetParticlesPrefab;

    public AudioSource SFX_HIT;
    public AudioSource SFX_KILL;
    public AudioSource SFX_RESET;

    public GameObject flashWhite;
    public GameObject flashRed;

    public static PlayerHealth healthScript {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        healthCount = 3;
        shake = Camera.main.GetComponent<ShakeBehaviour>();
        if (healthScript == null)
        {
            healthScript = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
                if (triangle != null)
                {
                    damageGiven(triangle.transform.position);
                    ThingSpawner.thingSpawnerInstance.activeThings.Remove(triangle);
                    Destroy(triangle);
                }
            }
        }
        powerupReady = true;
    }

    public void damageTaken(Vector3 particlesPos)
    {
        SFX_HIT.Play();
        StartCoroutine(flashHitRed());
        Instantiate(damageParticlesPrefab, particlesPos, Quaternion.identity);
    }

    public void damageGiven(Vector3 particlesPos)
    {
        SFX_KILL.Play();
        StartCoroutine(flashHitWhite());
        Instantiate(explosionParticlesPrefab, particlesPos, Quaternion.identity);
    }

    public void hexReset(Vector3 particlesPos)
    {
        SFX_RESET.Play();
        Instantiate(resetParticlesPrefab, particlesPos, Quaternion.identity);
    }

    IEnumerator flashHitWhite()
    {
        flashWhite.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        flashWhite.SetActive(false);
    }

    IEnumerator flashHitRed()
    {
        flashRed.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        flashRed.SetActive(false);
    }
}
