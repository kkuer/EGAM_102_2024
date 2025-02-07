using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThingScript : MonoBehaviour
{
    public enum ThingStates
    {
        Good,
        AtRisk,
        Dead
    }

    public ThingStates currentState;

    public bool isTouched = false;
    public bool hasBeenExecuted = false;

    public Color green;
    public Color yellow;
    public Color red;

    public Color touchedColor;

    public Animator animator;

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public PolygonCollider2D triggerCollider;

    public Coroutine greenToRedCoroutine;
    public Coroutine startCoroutine;
    public Coroutine touchedCoroutine;
    public Coroutine gameEnd;

    public float dyingDuration;
    public float speed;

    public bool canSwitch;
    public bool canStart;
    public bool animationStarted;
    public bool hasDied;
    public PlayerHealth healthManager;
    public Timer timer;

    public float minDuration;
    public float maxDuration;

    //

    // Start is called before the first frame update
    void Start()
    {
        canSwitch = true;
        canStart = true;
        animationStarted = false;
        hasDied = false;
        currentState = ThingStates.Good;

        transform.Rotate(0, 0, Random.Range(0, 360));

        //Apply a force to push the asteroid foward
        rb.AddForce(transform.up * speed);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {

            case ThingStates.Good:
                UpdateGood();

                break;
            case ThingStates.AtRisk:
                UpdateAtRisk();

                break;
            case ThingStates.Dead:
                UpdateDead();
                break;
        }
    }

    private void UpdateGood()
    {
        // set color to green 
        spriteRenderer.color = green;
        if (canSwitch)
        {
            startCoroutine = StartCoroutine(ExecuteStartTimer());
            canSwitch = false;
            canStart = true;
        }

        // after random number of seconds, switch to neutral state
        IEnumerator ExecuteStartTimer()
        {
            yield return new WaitForSeconds(Random.Range(minDuration, maxDuration));
            //animator.SetTrigger("SwitchStates");

            yield return new WaitForSeconds(0.1f);
            currentState = ThingStates.AtRisk;
            //Debug.Log("entering at risk state");
        }

        if (timer.hasWon == true)
        {
            Destroy(triggerCollider);
            StopAllCoroutines();
        }
    }
    private void UpdateAtRisk()
    {
        if (isTouched == false && canStart)
        {
            hasBeenExecuted = false;

            greenToRedCoroutine = StartCoroutine(ExecuteGreenToRed());

            animator.SetTrigger("pulsate");

            canStart = false;
        }

        // if trigger != null, gradually bring back to green (preferably at a faster rate than when it turned before)
        if (isTouched == true && hasBeenExecuted == false)
        {
            if (greenToRedCoroutine != null)
            {
                StopCoroutine(greenToRedCoroutine);
                
                animator.SetTrigger("idle");

                // start coroutine, set thing to yellow, pulsate for a couple seconds, then switch back to good state

                touchedCoroutine = StartCoroutine(ExecuteYellow());
            }
            hasBeenExecuted = true;
        }

        // coroutine
        IEnumerator ExecuteGreenToRed()
        {
            float timer = 0;

            while ( timer < dyingDuration)
            {
                timer += Time.deltaTime;

                float interp = timer / dyingDuration;
                Color color = Color.Lerp(yellow, red, interp);
                spriteRenderer.color = color;   

                // if trigger == false, after set number of seconds, switch to at risk state
                if (timer > dyingDuration)
                {
                    animator.SetTrigger("SwitchStates");
                    currentState = ThingStates.Dead; break;
                }

                yield return null;
            }
        }

        IEnumerator ExecuteYellow()
        {
            spriteRenderer.color = touchedColor;
            animator.SetTrigger("SwitchStates");

            yield return new WaitForSeconds(1.5f);
            canSwitch = true;
            currentState = ThingStates.Good;
        }

        if (timer.hasWon == true)
        {
            currentState = ThingStates.Good;
            Destroy(triggerCollider);
            StopAllCoroutines();
        }
    }
    private void UpdateDead()
    {
        // damage player once
        // destroy object

        spriteRenderer.color = Color.black;

        if (hasDied == false && timer.hasWon == false)
        {
            healthManager.healthCount -= 1;
            hasDied = true;
            Destroy(this);
        }

        if (timer.hasWon == true)
        {
            currentState = ThingStates.Good;
            Destroy(triggerCollider);
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isTouched = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isTouched = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "IgnoredByPlayer" || collision.gameObject.tag == "Wall")
        {
            rb.AddForce((transform.up * -1) * speed * 2);
        }
    }
}
