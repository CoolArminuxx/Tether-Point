using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scp_PowerOutlet : MonoBehaviour
{
    public GameObject RayStart;
    public GameObject[] RayEnds;
    public GameObject Effect1;
    public GameObject Effect2;
    public LayerMask PlayerMask;
    public int P;//Tether
    private Scene CurrentScene;

    private void Start()
    {
        CurrentScene = SceneManager.GetActiveScene();
        CheckTether();
    }
    private void Update()
    {
        if (CurrentScene.name == "End")
        {
            Cursor.visible = true;//enable cursor
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void CheckTether()
    {
        P = 0;//Set powered tethers value to 0
        foreach (GameObject rayend in RayEnds)//For every tether that the ray touches
        {
            P += 1;//Increase powered tethers value by 1
            Ray theRay = new Ray(RayStart.transform.position, rayend.transform.position - RayStart.transform.position);//Create ray from power box to specified location
            if (Physics.Raycast(theRay, out RaycastHit hit, 10, ~PlayerMask)) //If raycast has hit something whilst ignoring PlayerMask layer
            {
                if (hit.transform.gameObject.tag == "Tether")//If ray hits a tether
                {
                    if (P == 1)
                    {
                        Effect1.SetActive(true);//Activate visual effect connecting to first tether in range
                    }
                    if (P == 2)
                    {
                        Effect2.SetActive(true);//Activate visual effect connecting to second tether in range
                    }
                    hit.collider.gameObject.GetComponent<scp_Tether>().Powered = true;//Set powered bool to true in tether script
                    hit.collider.gameObject.GetComponent<scp_Tether>().CheckTethers();//Call on function to check for any nearby tethers
                    Debug.DrawRay(RayStart.transform.position, rayend.transform.position - RayStart.transform.position, Color.green);
                }
                else//If ray does not hit tether
                {
                    Debug.DrawRay(RayStart.transform.position, rayend.transform.position - RayStart.transform.position, Color.red);
                    if (P == 1)
                    {
                        Effect1.SetActive(false);//Deactivate effect
                    }
                    if (P == 2)
                    {
                        Effect2.SetActive(false);//Deactivate effect
                    }
                }
            }
            else//If ray does not hit anything
            {
                if (P == 1)
                {
                    Effect1.SetActive(false);//Deactivate effect
                }
                if (P == 2)
                {
                    Effect2.SetActive(false);//Deactivate effect
                }
            }
        }
    }
}
