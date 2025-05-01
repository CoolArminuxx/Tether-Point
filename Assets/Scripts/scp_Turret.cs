using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Turret : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Transform Turret;
    [SerializeField] bool FollowPlayer = false;
    [SerializeField] Animator anim;
    [SerializeField] float Number; //Blend tree number
    [SerializeField] float ChangeNumber;
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] Transform LaserSpawnPoint;
    [SerializeField] int LaserSpeed;
    [SerializeField] GameObject LaserShootAudio;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("MainCamera").transform; //Find the object in the scene that the turrets will target which is the camera on the player
        LaserShootAudio = GameObject.Find("LaserAudio");//Find the audio source which plays the turret projectile sound
    }
    private void FixedUpdate()
    {
        if (FollowPlayer == true)//Check if player is in range of the turrets
        {
            if (Number < 0.5) //Check if the threshold value is below 0.5 and motion in the blend tree is on "TurretDown"
            {
                IncreaseValue();//Call on method to increase Number value to smoothly transition up the blend tree animations
                Turret.LookAt(new Vector3(Player.position.x, Player.position.y, Player.position.z));//Turret rotation follows the players location
            }
            else//If value is higher than 0.5
            {
                Number = 1f;//Set value to 1 so that animation in the blend tree is set to play the shooting animation
                Turret.LookAt(new Vector3(Player.position.x, Player.position.y, Player.position.z));//Turret rotation follows the players location
            }
        }
        else//If player not in the turret range
        {
            if (Number > 0)//Check if turret animation is shooting
            {
                DecreaseValue();//Call on method to decrease Number value to smoothly transition back down the blend tree animations
            }
            else//if number is less than zero
            {
                Number = 0;//Set to zero
            }
        }
        anim.SetFloat("AnimNum", Number);//Set the "AnimNum" parameter value in the blend tree to the Number float value
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//If player is in the turret trigger range, set bool to true
        {
            FollowPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")//If player is not in the turret range, set bool to false and the animation blend to stop playing shooting animation
        {
            FollowPlayer = false;
            Number = 0.5f;
        }
    }
    void DecreaseValue()
    {
        Number -= Time.deltaTime / ChangeNumber;//Gradually decrease Number by time divided by ChangeNumber value
    }
    void IncreaseValue()
    {
        Number += Time.deltaTime;//Gradually increase Number by time
    }
    void TurretShoot()
    {
        if (FollowPlayer == true)//Check if player is in range
        {
            var Laser = Instantiate(LaserPrefab, LaserSpawnPoint.position, LaserSpawnPoint.rotation);//Instantiate projectile prefab
            Laser.GetComponent<Rigidbody>().velocity = LaserSpawnPoint.forward * LaserSpeed;//Get rigidbody from prefab and apply forward thrust to projectile
            LaserShootAudio.GetComponent<AudioSource>().Play();//Get audio source component from gameobject
        }
    }
}
