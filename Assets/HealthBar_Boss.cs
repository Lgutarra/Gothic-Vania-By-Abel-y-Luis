using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Boss : MonoBehaviour
{
    public Slider mSliderBossHealthBar;
    public float maxBossHealthBar;
    public float bossHealth;

    



    public static HealthBar_Boss BossHBinstance;

    public void Awake()
    {
        if(BossHBinstance == null)
        {
            BossHBinstance = this;
        }
    }
    

    void Start()
    {   
        Boss Boss = GameManager.Instance.boss;
        mSliderBossHealthBar = GetComponent<Slider>();


        mSliderBossHealthBar.maxValue = maxBossHealthBar;
        bossHealth = maxBossHealthBar;

        
        
        
    }

    
    void Update()
    {

    }

    

    public void Hurt()
    {
        bossHealth -= 25f;
        // Disminuir la vida en el slider
        mSliderBossHealthBar.value = bossHealth;

        if (bossHealth <= 0f)
        {
            // Morir
            //Boss.mCanvas_Boss.gameObject.SetActive(false);
            //Boss.mRb_Boss.velocity = Vector2.zero;
            //mAnimator.SetTrigger("Die");
        }
    }





}
