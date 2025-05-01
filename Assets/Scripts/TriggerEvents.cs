using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvents : MonoBehaviour
{
    [SerializeField] AudioSource PlayAudio;
    [SerializeField] GameObject ThisObject;
    [SerializeField] FirstPersonController PlayerFPC;
    [SerializeField] GameObject OutroScene;
    [SerializeField] Rigidbody PlayerRB;
    private bool Ending;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (ThisObject.tag == "End")//Once Player reaches end of tutorial level, freeze player in place and wait before audio ends then load next scene
            {
                Ending = true;
                PlayerFPC.playerCanMove = false;
                PlayerFPC.cameraCanMove = false;
                PlayerFPC.enableHeadBob = false;
                PlayerRB.constraints = RigidbodyConstraints.FreezePosition;
                PlayerRB.constraints = RigidbodyConstraints.FreezeRotation;
                PlayAudio.Play();
                StartCoroutine(waitForSound());
            }
            if (ThisObject.tag == "EndGame")//Once Player reaches end of demo level, freeze player then load end of game scene
            {
                PlayerFPC.playerCanMove = false;
                PlayerFPC.cameraCanMove = false;
                PlayerFPC.enableHeadBob = false;
                PlayerRB.constraints = RigidbodyConstraints.FreezePosition;
                PlayerRB.constraints = RigidbodyConstraints.FreezeRotation;
                OutroScene.SetActive(true);
            }
            if (Ending == false)//Check if game has not ended yet, play audio then disable the object this script is attached to
            {
                PlayAudio.Play();
                ThisObject.SetActive(false);
            }
        }
    }
    IEnumerator waitForSound()//Wait until audio has finished playing, then enable outro animation object
    {
        Debug.Log("Hello");
        while (PlayAudio.isPlaying)
        {
            yield return null;
        }
        OutroScene.SetActive(true);
    }
}
