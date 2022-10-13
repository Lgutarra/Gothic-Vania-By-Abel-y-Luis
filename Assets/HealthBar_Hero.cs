using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Hero : MonoBehaviour
{
    public Slider mSliderHeroHealthBar;
    public float maxHeroHealthBar;
    public float heroHealth;

    



    public static HealthBar_Hero HeroHBinstance;

    public void Awake()
    {
        if(HeroHBinstance == null)
        {
            HeroHBinstance = this;
        }
    }
    

    void Start()
    {   

        //HeroController hero = GameManager.Instance.hero;
        //Boss Boss = GameManager.Instance.boss;

        mSliderHeroHealthBar = GetComponent<Slider>();


        mSliderHeroHealthBar.maxValue = maxHeroHealthBar;
        heroHealth = maxHeroHealthBar;

        
        
        
    }

    
    void Update()
    {
        
    }

    

    public void Hurt()
    {
        heroHealth -= 10f;
        // Disminuir la vida en el slider
        mSliderHeroHealthBar.value = heroHealth;

        if (heroHealth <= 0f)
        {
            // Morir
            //Boss.mCanvas_Boss.gameObject.SetActive(false);
            //Boss.mRb_Boss.velocity = Vector2.zero;
            //mAnimator.SetTrigger("Die");
        }
    }





}
