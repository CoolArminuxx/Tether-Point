using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private PlayerHealth HealthRef;

    void Awake()
    {
        HealthRef = FindObjectOfType<PlayerHealth>();//Find player health script
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")//If laser projectile collides with player / Then remove 5 health, add 0.05 to alpha value / Then destroy this projectile
        {
            HealthRef.health -= 5;
            HealthRef.AlphaValue += 0.05f;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);//Destroy this projectile
        }
    }
}
