using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scp_AnimEvents : MonoBehaviour
{
    public GameObject Particle1;
    public GameObject Particle2;
    public GameObject Particle3;
    public GameObject FadeInOBJ;
    public GameObject FrostOBJ;
    public Animator FrostAnim;
    public Animator PodAnim;
    public FirstPersonController PlayerFPC;
    public GameObject IntroAudio;
    public int SceneNum;

    private void Start()
    {
        PlayerFPC.playerCanMove = false;//disable player movement
        PlayerFPC.cameraCanMove = false;//disable camera movement
    }
    public void ActivateFrost()//Activate all frost effects at the start of tutorial
    {
        FrostAnim.enabled = true;
        PodAnim.enabled = true;
        Particle1.SetActive(true);
        Particle2.SetActive(true);
        Particle3.SetActive(true);
    }
    public void DeactivateFrost()//Disable all of the frost effects and enable player movement
    {
        IntroAudio.SetActive(true);
        PlayerFPC.playerCanMove = true;
        PlayerFPC.cameraCanMove = true;
        FrostOBJ.SetActive(false);
        Particle1.SetActive(false);
        Particle2.SetActive(false);
        Particle3.SetActive(false);
        FadeInOBJ.SetActive(false);
    }
    public void Deactivate()//Deactivate black screen fade in
    {
        FadeInOBJ.SetActive(false);
    }
    public void Activate()//Activate black screen fade in
    {
        FadeInOBJ.SetActive(true);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneNum);//load specified scene
    }
}
