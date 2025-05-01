using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_Projectile : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SelfDestruct());//Once instantiated, timer is set before it gets destroyed
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Collision")//If projectile collides with any object / Then destroy projectile
        {
            Destroy(gameObject);
        }
    }
    IEnumerator SelfDestruct()//After 2 seconds of being active, destroy projectile
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
