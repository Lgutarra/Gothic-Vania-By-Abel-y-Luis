using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

	public static Boss BossInstance { get; private set; }
	//public static Boss BossInstance;
	[SerializeField]
    public GameObject prefabWaveStun;
	[SerializeField]
	public GameObject prefabAttack1;
	[SerializeField]
	public GameObject SpinEffect;

	[SerializeField]
	public GameObject ClimaEffect;

	private Transform mWaveSpawnPoint;
	public Transform hero;
	private Animator mAnimator;
	public Rigidbody2D mRb_Boss;

	public Transform mCanvas_Boss;
  
	public bool isFlipped = false;

	private void Awake()
    {
        BossInstance = this;
    }

	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > hero.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < hero.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}


	void Start()
    {
        mRb_Boss = GetComponent<Rigidbody2D>();
        mWaveSpawnPoint = transform.Find("WaveSpawnPoint");
		mAnimator = GetComponent<Animator>();
		mCanvas_Boss = transform.Find("Canvas");

		HeroController hero = GameManager.Instance.hero;

	}


	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HealthBar_Boss.BossHBinstance.Hurt();

			increasePowerBar();  
        }
        
    }





/*
	public void attack1()
	{
		Instantiate(
            prefabAttack1, 
            mBulletSpawnPoint.position, 
            Quaternion.identity
        );

	}

		
*/

	void Update()
	{/*
		if ((mAnimator.GetBool("ataqueDistancia")))
			{
					// Animacion de disparo
				//mAnimator.SetTrigger("shoot");
				ShootWave();
			} 
		*/

		if (HealthBar_Boss.BossHBinstance.bossHealth <= 500)
		{
			mAnimator.SetBool("SegundaFase", true);

			SpinEffect.SetActive(true);
			ClimaEffect.SetActive(true);
		}


	}


	public void ShootWave()
    {

        Instantiate(
            prefabWaveStun, 
            mWaveSpawnPoint.position, 
            Quaternion.identity
        );

		

		mAnimator.SetTrigger("perseguir");
    }

	public void PruebaEspera(){
		StartCoroutine(WaitShootWave());
	}
	IEnumerator WaitShootWave()
    {
        yield return new WaitForSeconds(1.5f);
        
    }

	

	public int GetPointDirectionBoss()
    {
        return (int)transform.localScale.x;
    }

	private void increasePowerBar()
    {   
        if (HeroController.HeroInstance.mSliderPowerBar.value >= 100)
        {
            HeroController.HeroInstance.mPower = 100;
        } else if (HeroController.HeroInstance.mSliderPowerBar.value <100 )
        {
            HeroController.HeroInstance.mPower += 25f;
        }

            
        HeroController.HeroInstance.mSliderPowerBar.value = HeroController.HeroInstance.mPower;
        
    }





}