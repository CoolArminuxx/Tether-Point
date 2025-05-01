using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Area1OBJ;
    [SerializeField] GameObject Area2OBJ;
    [SerializeField] GameObject Area3OBJ;
    public bool Area1BOOL;
    public bool Area2BOOL;
    public bool Area3BOOL;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//If player enters death trigger, check which checkpoint is active and then spawn at checkpoint
        {
            if (Area1BOOL == true)
            {
                other.transform.position = Area1OBJ.transform.position;
            }
            if (Area2BOOL == true)
            {
                other.transform.position = Area2OBJ.transform.position;
            }
            if (Area3BOOL == true)
            {
                other.transform.position = Area3OBJ.transform.position;
            }
        }
        if (other.gameObject.tag == "Box")//If box enters death trigger, check which checkpoint is active and then spawn at checkpoint
        {
            if (Area1BOOL == true)
            {
                other.transform.position = Area1OBJ.transform.position;
            }
            if (Area2BOOL == true)
            {
                other.transform.position = Area2OBJ.transform.position;
            }
        }
    }
    public void Checkpoint()//If player gets killed, check which checkpoint is active and then spawn at checkpoint
    {
        if (Area1BOOL == true)
        {
            Player.transform.position = Area1OBJ.transform.position;
        }
        if (Area2BOOL == true)
        {
            Player.transform.position = Area2OBJ.transform.position;
        }
        if (Area3BOOL == true)
        {
            Player.transform.position = Area3OBJ.transform.position;
        }
    }
}
