using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingSpawner : MonoBehaviour
{
    public static ThingSpawner thingSpawnerInstance { get; private set; }

    public GameObject spawnPrefab;
    public GameObject spawnPrefab2;

    public GameObject gameOver;
    public PlayerHealth healthManager;
    public Timer timer;

    public Coroutine spawnSquareCoroutine;
    public Coroutine spawnTriangleCoroutine;

    public Transform spawnOrigin;
    public Vector2 spawnArea;

    public int spawnAmount;
    public Vector2 boxSize;

    public List<GameObject> shapePrefabList;

    public List<GameObject> powerupsList;
    public bool powerupSpawnable = true;

    public List<GameObject> activeTriangles;
    public List<GameObject> activeThings;

    public bool triangleSpawnStarted;

    public AudioSource spawnSound;

    //public float thing;

    // Start is called before the first frame update
    void Awake()
    {
        if (thingSpawnerInstance == null)
        {
            thingSpawnerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        spawnSquareCoroutine = StartCoroutine(SpawnCoroutine());
        triangleSpawnStarted = false;
        powerupSpawnable = true;
    }

    IEnumerator SpawnCoroutine()
    {
        Vector2 originPoint = spawnOrigin.position;

        for (int i = 0; i < spawnAmount; i++)
        {
            Vector2 newPosition = Vector2.zero;

            bool isPositionOverlap = true;

            int attempts = 100;

            while (isPositionOverlap)
            {
                Vector2 randomOffset = Vector2.zero;
                randomOffset.x = Random.Range(-spawnArea.x, spawnArea.x);
                randomOffset.y = Random.Range(-spawnArea.y, spawnArea.y);

                newPosition = originPoint + randomOffset;

                isPositionOverlap = Physics2D.BoxCast(newPosition, boxSize, 0, Vector2.zero);

                attempts--;
                
                if (attempts <= 0)
                {
                    break;
                }
            }

            //int randomShapeIndex = Random.Range(0, shapePrefabList.Count);
            //GameObject spawnPrefab = shapePrefabList[randomShapeIndex];

            if (spawnPrefab.GetComponent<ThingScript>() == null)
            {
                spawnPrefab.GetComponent<DangerThingScript>().healthManager = healthManager;
                spawnPrefab.GetComponent<DangerThingScript>().timer = timer;
            }
            else if (spawnPrefab.GetComponent<ThingScript>() != null)
            {
                spawnPrefab.GetComponent<ThingScript>().healthManager = healthManager;
                spawnPrefab.GetComponent<ThingScript>().timer = timer;
            }

            //spawnSound.pitch = UnityEngine.Random.Range(1, 1.5f);
            //spawnSound.Play();
            GameObject newObject = Instantiate(spawnPrefab);
            newObject.transform.position = newPosition;

            activeThings.Add(newObject);

            if (i == spawnAmount -1)
            {
                Debug.Log("the coroutine should start now");
                if (spawnTriangleCoroutine == null && triangleSpawnStarted == false)
                {
                    spawnTriangleCoroutine = StartCoroutine(SpawnTriangleCoroutine());
                    triangleSpawnStarted = true;
                }
            }

            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator SpawnTriangleCoroutine()
    {
        Vector2 originPoint = spawnOrigin.position;

        for (int i = 0; i < 100; i++)
        {
            Vector2 newPosition = Vector2.zero;

            bool isPositionOverlap = true;

            int attempts = 100;

            // check if the spawn position collides with objects in scene
            while (isPositionOverlap)
            {
                Vector2 randomOffset = Vector2.zero;
                randomOffset.x = Random.Range(-spawnArea.x, spawnArea.x);
                randomOffset.y = Random.Range(-spawnArea.y, spawnArea.y);

                newPosition = originPoint + randomOffset;

                isPositionOverlap = Physics2D.BoxCast(newPosition, boxSize, 0, Vector2.zero);

                attempts--;
                if (attempts <= 0)
                {
                    break;
                }
            }

            // give the prefab its component
            if (spawnPrefab2.GetComponent<DangerThingScript>() == null)
            {
                spawnPrefab2.GetComponent<ThingScript>().healthManager = healthManager;
                spawnPrefab2.GetComponent<ThingScript>().timer = timer;
            }
            else if (spawnPrefab2.GetComponent<DangerThingScript>() != null)
            {
                spawnPrefab2.GetComponent<DangerThingScript>().healthManager = healthManager;
                spawnPrefab2.GetComponent<DangerThingScript>().timer = timer;
            }

            //spawnSound.pitch = UnityEngine.Random.Range(1, 1.5f);
            //spawnSound.Play();
            GameObject newObject = Instantiate(spawnPrefab2);
            newObject.transform.position = newPosition;

            activeTriangles.Add(newObject);

            yield return new WaitForSeconds(4f);
        }
    }

    public IEnumerator spawnPowerup()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));

        Vector2 originPoint = spawnOrigin.position;

        Vector2 randomOffset = Vector2.zero;
        randomOffset.x = Random.Range(-spawnArea.x, spawnArea.x);
        randomOffset.y = Random.Range(-spawnArea.y, spawnArea.y);

        Vector2 newPosition = originPoint + randomOffset;

        GameObject newPowerup = Instantiate(powerupsList[Random.Range(0, powerupsList.Count)], newPosition, Quaternion.identity);
        powerupSpawnable = true;
    }

    private void Update()
    {
        if (powerupSpawnable && triangleSpawnStarted)
        {
            powerupSpawnable = false;
            StartCoroutine(spawnPowerup());
        }

        if (timer.hasWon == true)
        {
            StopAllCoroutines();
        }
    }
}
