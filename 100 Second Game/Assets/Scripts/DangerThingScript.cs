using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ThingScript;

public class DangerThingScript : MonoBehaviour
{
    public enum ThingStates
    {
        Spawn,
        Set
    }

    public ThingStates currentState;

    public Rigidbody2D rb;
    public PolygonCollider2D triggerCollider;
    public SpriteRenderer spriteRenderer;

    public bool isTouchedBad;
    public bool isTouchedGood;

    public bool isActive;
    public float speed;
    public float delay;

    public Color red;
    public Color touched;

    public PlayerHealth healthManager;
    public Timer timer;

    public Coroutine delayCoroutine;

    public ShakeBehaviour shake;

    // Start is called before the first frame update
    void Start()
    {
        delay = 1f;
        currentState = ThingStates.Spawn;

        transform.Rotate(0, 0, Random.Range(0, 360));
        spriteRenderer.color = red;

        //Apply a force to push the asteroid foward
        rb.AddForce(transform.up * speed);
        
        triggerCollider.enabled = false;

        shake = Camera.main.GetComponent<ShakeBehaviour>();

        healthManager = PlayerHealth.healthScript;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case ThingStates.Spawn:
                UpdateSpawn();

                break;
            case ThingStates.Set:
                UpdateSet();

                break;
        }
    }

    void UpdateSpawn()
    {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            currentState = ThingStates.Set;
            triggerCollider.enabled = true;
        }
    }

    void UpdateSet()
    {
        if (isTouchedBad)
        {
            healthManager.healthCount -= 1;
            shake.TriggerShake();
            //spriteRenderer.color = touched;
            Destroy(this.gameObject);
        }
        if (isTouchedGood)
        {
            //spriteRenderer.color = touched;
            if (ThingSpawner.thingSpawnerInstance != null)
            {
                ThingSpawner.thingSpawnerInstance.activeThings.Remove(gameObject);
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && timer.hasWon == false)
        {
            isTouchedBad = true;
            healthManager.damageTaken(col.gameObject.transform.position);
            shake.TriggerShake();
        }
        if (col.gameObject.CompareTag("PlayerBlue"))
        {
            isTouchedGood = true;
            healthManager.damageGiven(col.gameObject.transform.position);
            shake.TriggerShake();
        }
    }

    //void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        //isTouched = false;
    //        spriteRenderer.color = red;
    //        Destroy(this);
    //    }
    //    if (col.gameObject.CompareTag("PlayerBlue"))
    //    {
    //        isTouchedGood = false;
    //    }
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "IgnoredByPlayer" || collision.gameObject.tag == "Wall")
        {
            rb.AddForce((transform.up * -1) * speed * 2);
        }
    }

    //IEnumerator Delay()
    //{
    //    yield return new WaitForSeconds(1f);

    //    triggerCollider.enabled = true;
    //    isActive = true;
    //}
}
