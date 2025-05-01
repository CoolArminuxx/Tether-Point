using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Tether : MonoBehaviour
{
    //Total raycast lines and current rotation
    public int RayLines1 = 4;
    public int RayLines2 = 4;
    public int RayLines3 = 4;
    public int RayLines4 = 4;
    public int RayRotation1 = 0;
    public int RayRotation2 = 45;
    public int RayRotation3 = 0;
    public int RayRotation4 = 0;

    public bool Powered;                //Check if tether is powered
    public GameObject[] RayEndDOWN;     //Store all possible connections in the down facing ray lines
    public GameObject[] RayEndUP;       //Store all possible connections in the upwards facing ray lines
    public GameObject[] Connections;    //Store connections effects for the up and down facing ray lines
    public LayerMask PlayerMask;        //Store ignored layermask so that rays can go through certain objects

    //All electric connection effects
    public GameObject C1;
    public GameObject C2;
    public GameObject C3;
    public GameObject C4;
    public GameObject C5;
    public GameObject C6;
    public GameObject C7;
    public GameObject C8;

    private void Start()
    {
        CheckForColliders();//Check nearby tether connections
    }
    private void Update()
    {
        PowerCheck();//Check if tether is currently powered
    }
    public void CheckTethers()//Turn off all tether connections / Set all rotations values back to start / Then check for tether connections
    {
        DeactivateTethers();
        RayRotation1 = 0;
        RayRotation2 = 45;
        RayRotation3 = 0;
        RayRotation4 = 0;
        CheckForColliders();
    }
    void CheckForColliders()//Creats 8 raycasts on a horzontal level and 4 on a slanted vertical level / Raycast only activates once to check for tethers then deactivates
    {
        if (Powered == true)//Check if tether is connected to a powered tether
        {
            for (int i = 0; i < RayLines1; i++)//creates a raycast then change rotation to create another raycast 4 times in a '+' pattern
            {
                Quaternion q = Quaternion.AngleAxis(RayRotation1, Vector3.up);
                Vector3 d = transform.forward * 10;
                Ray theRay = new Ray(transform.position, q * d);
                if (Physics.Raycast(theRay, out RaycastHit hit, 10, ~PlayerMask))//Check if ray hits something whilst ignoring the player mask
                {
                    if (hit.transform.gameObject.tag == "Tether")//Check ray hits a tether
                    {
                        hit.collider.gameObject.GetComponent<scp_Tether>().Powered = true;
                        Debug.DrawRay(transform.position, q * d, Color.green);
                        switch (RayRotation1)//activates an electric effect for every tether that the ray hits
                        {
                            case 0:
                                C1.SetActive(true);
                                break;
                            case 90:
                                C3.SetActive(true);
                                break;
                            case 180:
                                C5.SetActive(true);
                                break;
                            case 270:
                                C7.SetActive(true);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, q * d, Color.red);//hit an object that is not a tether
                    }
                }
                RayRotation1 += 90;
            }
            for (int i = 0; i < RayLines2; i++)//creates a raycast then change rotation to create another raycast 4 times in an 'X' pattern
            {
                Quaternion q = Quaternion.AngleAxis(RayRotation2, Vector3.up);
                Vector3 d = transform.forward * 14;
                Ray theRay = new Ray(transform.position, q * d);
                if (Physics.Raycast(theRay, out RaycastHit hit, 14, ~PlayerMask))//Check if ray hits something whilst ignoring the player mask / ray has a slightly further reach
                {
                    if (hit.transform.gameObject.tag == "Tether")//Check ray hits a tether
                    {
                        hit.collider.gameObject.GetComponent<scp_Tether>().Powered = true;
                        Debug.DrawRay(transform.position, q * d, Color.green);
                        switch (RayRotation2)//activates an electric effect for every tether that the ray hits
                        {
                            case 45:
                                C2.SetActive(true);
                                break;
                            case 135:
                                C4.SetActive(true);
                                break;
                            case 225:
                                C6.SetActive(true);
                                break;
                            case 315:
                                C8.SetActive(true);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, q * d, Color.red);
                    }
                }
                RayRotation2 += 90;
            }
            if (RayEndDOWN.Length > 0)//Rays point slightly down to detect tethers on a lower elevation
            {
                for (int i = 0; i < RayLines3; i++)//creates a raycast then change rotation to create another raycast 4 times in a down facing '+' pattern
                {
                    Quaternion q = Quaternion.AngleAxis(RayRotation3, Vector3.up);
                    Vector3 d = RayEndDOWN[0].transform.position * 20;
                    Ray theRay = new Ray(transform.position, q * d);
                    if (Physics.Raycast(theRay, out RaycastHit hit, 20, ~PlayerMask))//Check if ray hits something whilst ignoring the player mask / Ray has further reach down
                    {
                        if (hit.transform.gameObject.tag == "Tether")//Check ray hits a tether
                        {
                            hit.collider.gameObject.GetComponent<scp_Tether>().Powered = true;
                            Debug.DrawRay(transform.position, q * d, Color.green);
                            switch (RayRotation3)//activates an electric effect for every tether that the ray hits
                            {
                                case 0:
                                    Connections[0].SetActive(true);
                                    Debug.Log("1");
                                    break;
                                case 90:
                                    Connections[1].SetActive(true);
                                    Debug.Log("2");
                                    break;
                                case 180:
                                    Connections[2].SetActive(true);
                                    Debug.Log("3");
                                    break;
                                case 270:
                                    Connections[3].SetActive(true);
                                    Debug.Log("4");
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Debug.DrawRay(transform.position, q * d, Color.red);
                        }
                    }
                    RayRotation3 += 90;
                }
            }
            if (RayEndUP.Length > 0)//Rays point slightly up to detect tethers on a higher elevation
            {
                for (int i = 0; i < RayLines4; i++)//creates a raycast then change rotation to create another raycast 4 times in an upwards facing '+' pattern
                {
                    Quaternion q = Quaternion.AngleAxis(RayRotation4, Vector3.up);
                    Vector3 d = RayEndUP[0].transform.position * 20;
                    Ray theRay = new Ray(transform.position, q * d);
                    if (Physics.Raycast(theRay, out RaycastHit hit, 20, ~PlayerMask))//Check if ray hits something whilst ignoring the player mask / Ray has further reach up
                    {
                        if (hit.transform.gameObject.tag == "Tether")//Check ray hits a tether
                        {
                            hit.collider.gameObject.GetComponent<scp_Tether>().Powered = true;
                            Debug.DrawRay(transform.position, q * d, Color.green);
                            switch (RayRotation4)//activates an electric effect for every tether that the ray hits
                            {
                                case 0:
                                    Connections[0].SetActive(true);
                                    Debug.Log("1");
                                    break;
                                case 90:
                                    Connections[1].SetActive(true);
                                    Debug.Log("2");
                                    break;
                                case 180:
                                    Connections[2].SetActive(true);
                                    Debug.Log("3");
                                    break;
                                case 270:
                                    Connections[3].SetActive(true);
                                    Debug.Log("4");
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Debug.DrawRay(transform.position, q * d, Color.red);
                        }
                    }
                    RayRotation4 += 90;
                }
            }
        }
    }
    void DeactivateTethers()//Deactivate all effects
    {
        C1.SetActive(false);
        C2.SetActive(false);
        C3.SetActive(false);
        C4.SetActive(false);
        C5.SetActive(false);
        C6.SetActive(false);
        C7.SetActive(false);
        C8.SetActive(false);

        if (Connections.Length > 0)//Check if there are any connections in the array / Deactivate all connections in array
        {
            Connections[0].SetActive(false);
            Connections[1].SetActive(false);
            Connections[2].SetActive(false);
            Connections[3].SetActive(false);
        }
    }
    void PowerCheck()//Check if tether is powered, then check for any nearby tethers to power
    {
        if (Powered == true && RayRotation1 == 0)
        {
            CheckTethers();
        }
    }
}
