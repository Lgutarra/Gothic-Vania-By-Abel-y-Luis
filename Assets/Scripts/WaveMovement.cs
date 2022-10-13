using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WaveMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float secondsToDestroy = 1f;
    
    private Rigidbody2D mRb; 
    private float timer = 0f;

    void Start()
    {
        mRb = GetComponent<Rigidbody2D>();

        Boss Boss = GameManager.Instance.boss;
           
        if (Boss.isFlipped == true)   
        {
            mRb.velocity = new Vector2(
            speed, 
            0f);
            transform.Rotate(0f, 180f, 0f);
        }else{
            mRb.velocity = new Vector2(
            -speed, 
            0f);
        }
       
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > secondsToDestroy)
        {
            // Destruir la bala
            Destroy(gameObject);
        }
    }
/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
*/

}
