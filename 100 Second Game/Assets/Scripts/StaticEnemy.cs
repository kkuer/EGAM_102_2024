using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public ShakeBehaviour shake;

    public PlayerHealth healthManager;

    // Start is called before the first frame update
    void Start()
    {
        shake = Camera.main.GetComponent<ShakeBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerBlue")
        {
            healthManager.damageTaken(other.gameObject.transform.position);
            healthManager.healthCount -= 1;
            shake.TriggerShake();
        }
    }
}
