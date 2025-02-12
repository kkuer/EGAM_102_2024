using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehaviour : MonoBehaviour
{
    public enum powerUp
    {
        HexReset,
        EnemyKill
    }

    public powerUp type;
    public ShakeBehaviour shake;

    // Start is called before the first frame update
    void Start()
    {
        shake = Camera.main.GetComponent<ShakeBehaviour>();
    }
    private void OnCollisionEnter2D(Collision2D player)
    {
        Debug.Log("collided");
        PlayerHealth playerHealth = player.gameObject.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null && playerHealth.powerupReady == true)
        {
            Debug.Log("collided with player");
            if (this.type == powerUp.HexReset)
            {
                playerHealth.applyPowerupHex();
            }
            else if (this.type == powerUp.EnemyKill)
            {
                playerHealth.applyPowerupEnemy();
                Destroy(this.gameObject);
            }
        }
    }
}
