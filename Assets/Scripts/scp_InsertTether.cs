using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_InsertTether : MonoBehaviour
{
    //Attached to every tether node / Variables are referenced in different scripts
    public scp_Pickup PickupRef;
    private bool TetherBool;
    public GameObject Tether;
    [SerializeField] GameObject TetherHand;
    [SerializeField] GameObject TetherUI;
}
