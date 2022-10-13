using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    public static HeroController HeroInstance { get; private set; }
    
 
    
    [SerializeField]
    private float maxHealth;
    
    
    public float speed = 7;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float raycastDistance;
    [SerializeField]
    private GameObject prefabBullet;

    [SerializeField]
    private GameObject Square;
    [SerializeField]
    private GameObject EndTeleportEffect;

    [SerializeField]
    public float maxPower;

    public float mPower;

    public Slider mSliderPowerBar;

    public float mMovement = 0f;    
    private bool mIsJumpPressed = false;
    private bool mIsJumping = false;
    private bool mIsFalling = false;
    private Rigidbody2D mRb;
    private Transform mRaycastPoint;
    private CapsuleCollider2D mCollider;
    private Vector3 mRaycastPointCalculated;
    private Animator mAnimator;
    private Transform mBulletSpawnPoint;

    //numSaltos es una variable auxiliar para permitir dos saltos seguidos -abel
    private int numSaltos = 0;


    
    private void Awake()
    {
        HeroInstance = this;
    }


    public void Teleport()
    {   
        float directionTeleport= transform.localScale.x;

         mPower -= 100f;
        // Disminuir la vida en el slider
        mSliderPowerBar.value = mPower;

        
        Vector3 movement_prueba = new Vector3(
            directionTeleport * 10f,
            0f,
            0f);
        transform.Translate(movement_prueba);
        EndTeleportEffect.SetActive(false);
        Square.SetActive(false);
        StartCoroutine(WaitTeleportEffect());
        
        

    }

    IEnumerator WaitTeleportEffect()
    {

        EndTeleportEffect.SetActive(true);
        Square.SetActive(true);
        yield return new WaitForSeconds(0.65f);
        EndTeleportEffect.SetActive(false);
        Square.SetActive(false);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WeaponBoss"))
        {
            HealthBar_Hero.HeroHBinstance.Hurt();

        }
    }
        
    


    void Start()
    {
        mRb = GetComponent<Rigidbody2D>();
        mRaycastPoint = transform.Find("RaycastPoint");
        mCollider = GetComponent<CapsuleCollider2D>();
        mAnimator = GetComponent<Animator>();
        mBulletSpawnPoint = transform.Find("BulletSpawnPoint");

        mSliderPowerBar = transform.Find(
            "Canvas"
        ).Find(
            "PowerBar"
        ).Find(
            "Border"
        ).GetComponent<Slider>();

        
        mPower = 0;
        mSliderPowerBar.maxValue = maxPower;

    }

    void FixedUpdate()
    {

        //transform.position += mMovement * speed * Time.fixedDeltaTime * Vector3.right;
        mRb.velocity = new Vector2(
            mMovement * speed,
            mRb.velocity.y
        );

        if (mRb.velocity.x != 0f)
        {
            transform.localScale = new Vector3(
                mRb.velocity.x < 0f ? -1f : 1f,
                transform.localScale.y,
                transform.localScale.z
            );

  
        }

        IsJumping();

        if (mIsJumpPressed)
        {
            // Comenzar salto
            Jump();
        }

        // Informativo
        Debug.DrawRay(
            mRaycastPointCalculated,
            Vector2.down * raycastDistance,
            mIsJumping == true ? Color.green : Color.white
        );
    }

    void Update()
    {


        /* teleport
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(movement);   
*/





        mMovement = Input.GetAxis("Horizontal");

        if (mMovement > 0f || mMovement < 0f )
        {
            mAnimator.SetBool("isMoving", true);
        }else
        {
            mAnimator.SetBool("isMoving", false);
        }


        if (Input.GetKeyDown(KeyCode.F))
        {   
            if ( mSliderPowerBar.value == 100) 
            {
                Teleport();
            }
            
        } 

       
        /* Aqui se establece que cuando se presione Espacio y el numero de saltos 
        sea 2 o menor entonces va a permitir un salto. Cuando el numero de saltos 
        sea 2, ya no permitira saltar -Abel*/ 

        
        if ( numSaltos < 1 && Input.GetKeyDown(KeyCode.Space))
        {   //esta variable de numSaltos se vuelve igual a 0 cuando el heroe toca el piso
            // Dentro del If (hit) de la funcion o metodo IsJumping, se establecio esa sentencia - Abel
            numSaltos = numSaltos + 1;
            mIsJumpPressed = true;

            
        }        

        if (Input.GetMouseButtonDown(0))
        {
            // Animacion de disparo
            mAnimator.SetTrigger("shoot");
            Fire();
        } 

        
        RaycastHit2D hit = Physics2D.Raycast(
            mRaycastPointCalculated,// Posicion origen
            Vector2.down,// Direccion
            raycastDistance// Distancia
        );
        if (hit)
        {   
            mAnimator.ResetTrigger("Fall");
            mAnimator.SetTrigger("hitGround");
            numSaltos = 0;
        }else 
        {
            mAnimator.ResetTrigger("hitGround");
        }

        if (mRb.velocity.y < 0f)
        {
             mAnimator.SetTrigger("Fall");
             mAnimator.ResetTrigger("Jump");
        }

        if (mRb.velocity.y > 0f)
        {
             mAnimator.SetTrigger("Jump");
            if (numSaltos==1) 
            {   
                mAnimator.ResetTrigger("Jump");
                mAnimator.SetTrigger("SecondJump");
            }else {
                mAnimator.ResetTrigger("SecondJump");
            }

        }
        

        mAnimator.SetBool("isJumping", mIsJumping);
        //mAnimator.SetBool("isFalling", mRb.velocity.y < 0f);
        mAnimator.SetBool("isFalling", mIsFalling);


         




    }

    private void Jump()
    {
        mRb.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
        mIsJumping = true;
        mIsJumpPressed = false;
    }

    private void IsJumping()
    {
        mRaycastPointCalculated = new Vector3(
            mCollider.bounds.center.x,
            mCollider.bounds.center.y - mCollider.bounds.extents.y,
            transform.position.z
        );

        RaycastHit2D hit = Physics2D.Raycast(
            mRaycastPointCalculated,// Posicion origen
            Vector2.down,// Direccion
            raycastDistance// Distancia
        );
        if (hit)
        {
            // Hay una colision, esta en el suelo
            mIsJumping = false;
            //Ahora que el heroe ha tocado el piso, el numero de saltos vuelve a 0
            numSaltos = 0;
        }
    }

    private void Fire()
    {
        Instantiate(
            prefabBullet, 
            mBulletSpawnPoint.position, 
            Quaternion.identity
        );
    }

    public int GetPointDirection()
    {
        return (int)transform.localScale.x;
    }

   
    


}
