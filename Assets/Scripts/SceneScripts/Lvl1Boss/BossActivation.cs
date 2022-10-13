using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{   
    

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {   
            Debug.Log("Hero colisiono con Trigger");
            BossUI.instance.BossActivator();
            StartCoroutine(WaitForBoss());
        } else {
             Debug.Log("Hero no colisiono con Trigger");
        }
    }


    IEnumerator WaitForBoss()
    {
        var currentSpeed = HeroController.HeroInstance.speed;
        HeroController.HeroInstance.speed=0;
        yield return new WaitForSeconds(1f);
        HeroController.HeroInstance.speed=currentSpeed;
        Destroy(gameObject);
    }
    
}
