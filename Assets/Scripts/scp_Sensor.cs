using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Sensor : MonoBehaviour
{
    public GameObject RedLine;
    public GameObject GreenLine;
    [SerializeField] AudioSource LevelAudio;
    [SerializeField] GameObject PreviousAudio;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")//Check if projectile has collided with power sensor / Then set all red lines to green which are connected to this sensor
        {
            GreenLine.SetActive(true);
            RedLine.SetActive(false);
            LevelAudio.Play();
            PreviousAudio.SetActive(false);
        }
    }
}
