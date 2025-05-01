using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Door : MonoBehaviour
{
	public GameObject Door;
	public GameObject Sensor1;
	public GameObject Sensor2;
	public GameObject Sensor3;
	public GameObject Sensor4;
	public Animator anim;
	public OnDeath DeathRef;
	public GameObject RailTurretPrefab;
	public Transform SpawnLocation;
	private bool InstantiateOnce;

    private void Start()
    {
		InstantiateOnce = true;//Instantiate turret enemy once
    }
    private void Update()
	{
		if (Door.tag == "One" && Sensor1.activeSelf == true)//Check if door only has once sensor connected to it and if it has been activated
        {
			anim.SetBool("isOpen", true);
			DeathRef.Area1BOOL = true;//Set Checkpoint
			DeathRef.Area2BOOL = false;
			DeathRef.Area3BOOL = false;
		}
		if (Door.tag == "Two" && Sensor1.activeSelf == true && Sensor2.activeSelf == true)//Check if door has two sensors connected to it and if they are activated
		{
			anim.SetBool("isOpen", true);
			DeathRef.Area1BOOL = false;
			DeathRef.Area2BOOL = true;//Set Checkpoint
			DeathRef.Area3BOOL = false;
		}
		if (Door.tag == "Four" && Sensor1.activeSelf == true && Sensor2.activeSelf == true && Sensor3.activeSelf == true && Sensor4.activeSelf == true)//Check if door has four sensors connected and are activated
		{
			anim.SetBool("isOpen", true);
			DeathRef.Area1BOOL = false;
			DeathRef.Area2BOOL = false;
			DeathRef.Area3BOOL = true;//Set Checkpoint
			if (InstantiateOnce == true)//Instantiate turret prefab once on the specified location
            {
				Instantiate(RailTurretPrefab, SpawnLocation.position, Quaternion.identity);
				InstantiateOnce = false;
			}
		}
	}
}
