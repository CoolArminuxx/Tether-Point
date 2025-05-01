using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Tool : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject projectile;
    [SerializeField] float Velocity = 700f;
    [SerializeField] GameObject ToolPanel;
    [SerializeField] GameObject RayLocation;
    [SerializeField] AudioSource ShootAudio;
    public bool CanShoot;

    public float radius;
    public float maxDistance;
    public LayerMask layerMask;

    void Start()
    {
        
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);//Create an invisible sphere which detects and stores any tethers that are currently within the sphere cast
        for (int i = 0; i < hitColliders.Length; i++)
        {
            GameObject hitCollider = hitColliders[i].gameObject;
            if (hitCollider.CompareTag("Tether"))
            {
                if (hitCollider.GetComponent<scp_Tether>().Powered == true)//Check if the stored tether is currently powered
                {
                    CanShoot = true;//true if tether is powered
                }
                else
                {
                    CanShoot = false;//false if tether is not powered
                }
            }
        }
        if (hitColliders.Length <= 0)//Check if there are no tethers stored
        {
            CanShoot = false;//false if array is empty
        }
        if (CanShoot == true)
        {
            ToolPanel.SetActive(true);//Show indication that tool is active
            if (Input.GetMouseButtonDown(0))               //Check if the player has pressed the left mouse button
            {
                Shoot();                                   //Call on shoot method which controls the shooting mechanic
            }
        }
        else
        {
            ToolPanel.SetActive(false);//Disable indication to show tool is not powered
        }
    }

    void OnDrawGizmosSelected()//Testing purposes to check the range of the sphere cast
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Shoot()
    {
        GameObject Pulse = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);//Instantiate tool projectile
        Pulse.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, Velocity, 0));//Get rigidbody from prefab and apply forward velocity
        ShootAudio.Play();//Play audio clip
    }
}
