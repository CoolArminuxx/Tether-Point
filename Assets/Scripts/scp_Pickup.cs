using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Pickup : MonoBehaviour
{
    [SerializeField] bool ToolPicked = false;       //Store bool value to check if player has picked up tool
    [SerializeField] GameObject Tool;               //Store tool prefab
    [SerializeField] GameObject ToolOutline;        //Store object which contains tool outline script
    [SerializeField] GameObject ButtonUI;           //Store button instruction UI object for pickup tool
    [SerializeField] GameObject Reticle;            //Store reticle UI
    [SerializeField] Animator FloorDropAnim;        //Store Floor drop animation from starting area
    [SerializeField] AudioSource ToolInfoAudio;     //Store tool monologue audio
    [SerializeField] GameObject InfoAudio;          //Store gameobject which contains general information monologue audio
    [SerializeField] GameObject DecoyTool;          //Store Decoy tool from the pedestal
    [SerializeField] GameObject RealTool;           //Store real tool object
    [SerializeField] AudioSource InfoAudioSource;   //Store general information audio
    [SerializeField] AudioSource TetherAudio;       //Store tether sound
    [SerializeField] AudioSource NodeAudio;         //Store node sound

    public bool TetherPicked = false;           //Store bool to check if tether is currently being held
    [SerializeField] GameObject Tether;         //Store tether game object which will appear in the players hand
    [SerializeField] Outline TetherOutline;     //Store outline script
    [SerializeField] GameObject TetherUI;       //Store tether ui
    [SerializeField] GameObject TetherPlaceUI;  //Store tether ui to place down tether

    private scp_Tether[] TetherObjects;         //Store all tether scripts from scene in array
    private scp_InsertTether InsertTetherRef;   //Store Insert tether script
    private scp_PowerOutlet[] PowerRef;         //Store every power script in array
    public scp_Tool ToolRef;                    //Store tool script
    private float range = 3;                    //Store raycast length range

    [SerializeField] Transform holdArea;                  //Store location which prop will be held in
    private GameObject heldObj;                           //Store object which will be held
    private Rigidbody heldObjRB;                          //Store rigidbody of held object
    private bool HoldingBox;                              //Check if box is currently held
    [SerializeField] private float pickupRange = 5.0f;    //Distance between player and prop
    [SerializeField] private float pickupForce = 150.0f;  //How fast prop travels to towards player
    [SerializeField] GameObject BoxUI;                    //Store ui about picking up box
    [SerializeField] Outline BoxOutline;                  //Store box outline script

    void Start()
    {
        TetherObjects = Resources.FindObjectsOfTypeAll<scp_Tether>();//Find and store every tether script in array
        PowerRef = Resources.FindObjectsOfTypeAll<scp_PowerOutlet>();//Find and store every power script in array
    }

    void Update()
    {
        Vector3 direction = Vector3.forward;                                                         //Store direction in which the ray will travel
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));   //Create ray cast from this objects transform
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range));          //Draw raycast line in scene view

        if (Physics.Raycast(theRay, out RaycastHit hit, range))                   //Check if raycast has hit an object
        {
            if (hit.transform.gameObject.tag == "tool" && ToolPicked == false)    //Check if raycast has hit the tool
            {
                ToolOutline.GetComponent<Outline>().enabled = true; //Enable outline
                ButtonUI.SetActive(true);                  //Activate button UI for picking up the tool
                if (Input.GetKeyDown(KeyCode.E))           //Check if the E key has been pressed
                {
                    ToolInfoAudio.Play();                  //Play audio
                    StartCoroutine(waitForSound());        //Wait for audio to finish
                    ButtonUI.SetActive(false);             //Deactivate button UI for picking up the tool
                    Reticle.SetActive(true);               //Activate reticle
                    ToolPicked = true;                     //Player has tool in their hand
                    Tool.SetActive(true);                  //Activate tool prefab
                    Destroy(hit.transform.gameObject);     //Destroy tool object that was on the table
                }
            }
            if (hit.transform.gameObject.tag == "Tether" && TetherPicked == false)    //Check if raycast has hit the tether
            {
                TetherOutline = hit.collider.GetComponent<Outline>();//Get outline script from tether
                TetherOutline.enabled = true;              //activate outline on tether
                TetherUI.SetActive(true);                  //Activate tether UI for picking up the tether
                if (Input.GetKeyDown(KeyCode.E))           //Check if the E key has been pressed
                {
                    TetherAudio.Play();                         //Play tether sound
                    TetherOutline.enabled = false;              //Disable tether outline
                    hit.transform.gameObject.SetActive(false);  //Deactivate tether object that was on the floor
                    TetherUI.SetActive(false);                  //Deactivate tether UI for picking up the tether
                    TetherPicked = true;                        //Player has tether in their hand
                    Tether.SetActive(true);                     //Activate tether object
                    foreach (scp_Tether tether in TetherObjects)//for every tether script in array, disable all power / Then check if any tethers next to power box / Then check for nearby powered tethers
                    {
                        tether.Powered = false;
                        foreach (scp_PowerOutlet outlet in PowerRef)
                        {
                            outlet.CheckTether();
                        }
                        tether.CheckTethers();
                    }
                }
            }
            if (hit.transform.gameObject.tag == "TetherNode" && TetherPicked == true)    //Check if raycast has hit the tether node
            {
                InsertTetherRef = hit.collider.GetComponent<scp_InsertTether>();         //Get InsertTether script from node
                if (InsertTetherRef.Tether.active == false)                              //Check if there are no tethers in node slot
                {
                    TetherPlaceUI.SetActive(true);                  //Activate tether UI for placing down the tether
                    if (Input.GetKeyDown(KeyCode.E))                //Check if the E key has been pressed
                    {
                        NodeAudio.Play();
                        InsertTetherRef.Tether.SetActive(true);
                        foreach (scp_Tether tether in TetherObjects)//for every tether script in array, disable all power / Then check if any tethers next to power box / Then check for nearby powered tethers
                        {
                            foreach (scp_PowerOutlet outlet in PowerRef)
                            {
                                outlet.CheckTether();
                            }
                            tether.CheckTethers();
                        }
                        TetherPlaceUI.SetActive(false);         //Deactivate tether UI for picking up the tether
                        TetherPicked = false;                   //Player has tether in their hand
                        Tether.SetActive(false);                //Activate tether object
                    }
                }
            }
        }
        else
        {
            ButtonUI.SetActive(false);                 //Deactivate button UI for picking up the tool
            TetherUI.SetActive(false);                 //Deactivate tether UI for picking up the tether
            TetherPlaceUI.SetActive(false);            //Deactivate tether UI for placing down tether
            if (ToolPicked == false)                                 //If tool is not equipped
            {
                ToolOutline.GetComponent<Outline>().enabled = false; //Enable outline on tool
            }
            if (TetherUI.activeInHierarchy == false)                 //If ui not active
            {
                TetherOutline.enabled = false;                       //outlines is disabled
            }
        }
        if (ToolRef.isActiveAndEnabled && ToolRef.CanShoot == true)  //If script is enabled and player can shoot / Player can detect if they are facing a box and can hold it when pressing down RMB
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
            {
                BoxOutline.enabled = false;
                if (hit.transform.gameObject.tag == "Box")//Check if ray hits box object
                {
                    BoxUI.SetActive(true);
                    BoxOutline = hit.collider.GetComponent<Outline>();
                    BoxOutline.enabled = true;
                    if (Input.GetMouseButtonDown(1))//Check if RMB is pressed down
                    {
                        BoxUI.SetActive(false);
                        BoxOutline.enabled = false;
                        if (heldObj == null)
                        {
                            HoldingBox = true;
                            BoxOutline.enabled = false;
                            PickupObject(hit.transform.gameObject);//Call on pickup function to pickup box
                        }
                        else
                        {
                            DropObject();//Call on drop function to drop box
                        }
                    }
                }
            }
            else//Disable outline and UI if ray does not hit anything
            {
                BoxUI.SetActive(false);
                BoxOutline.enabled = false;
            }
            if (heldObj != null)//If object is held
            {
                MoveObject();//Call on move function to move box around
            }
        }
        else//If tool has been disabled
        {
            if (HoldingBox == true)//Check if currently holding box / Then drop the box and disable ui and outline for box
            {
                DropObject();
                HoldingBox = false;
                BoxUI.SetActive(false);
                BoxOutline.enabled = false;
            }
        }
        if (ToolPicked == false)//Check if tool has not been picked up yet
        {
            if (InfoAudio.activeInHierarchy == true)//Check if info audio is active in scene
            {
                StartCoroutine(waitForSound2());//Wait till audio has ended
            }
        }
    }
    void MoveObject()//Controls the distance between the held object and the held area so that the object will always move towards holding area by the determined force
    {
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }
    void PickupObject(GameObject pickObj)//Modify rigidbody of object so that the object is light, does not rotate, does not use gravity and contains some amount of drag force when moving it around
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
            heldObjRB.mass = 20;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }
    void DropObject()//Revert the properties of the object ridigbody back to its original state and has normal gravity
    {
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;
        heldObjRB.mass = 200;

        heldObj.transform.parent = null;
        heldObj = null;
    }
    IEnumerator waitForSound()//Wait for audio to finish playing
    {
        while (ToolInfoAudio.isPlaying)
        {
            yield return null;
        }

        FloorDropAnim.enabled = true;
    }
    IEnumerator waitForSound2()//Wait for audio to finish playing
    {
        while (InfoAudioSource.isPlaying)
        {
            yield return null;
        }

        DecoyTool.SetActive(false);
        RealTool.SetActive(true);
        InfoAudio.SetActive(false);
    }
}
