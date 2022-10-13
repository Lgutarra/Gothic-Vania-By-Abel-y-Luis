using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float health;
    [SerializeField]
    public float maxHealth;

    bool isInmune;
    [SerializeField]
    public float inmunityTime;

    //variable para el heroe cuando es golpeado
    Blink material;
    SpriteRenderer sprite;
    void Start()
    {   
        sprite= GetComponent<SpriteRenderer>();
        material = GetComponenet<Blink>();
        health = maxHealth; 
    }

    // Update is called once per frame
    private void Update()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInmune)
        {
            health -= collision.GetComponent<Enemy>().damageToGive;
            StartCoroutine(Inmunity);
            if(health <= 0)
            {
                //aparecer pantalla de game over
                print("player dead");
            }
        }
    }

    IEnumerator Inmunity()
    {
        isInmune = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(inmunityTime);
        sprite.material = material.original;
        isInmune = false;
    }


}
