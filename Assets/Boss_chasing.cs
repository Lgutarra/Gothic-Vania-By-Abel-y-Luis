using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_chasing : StateMachineBehaviour
{   
    public float speed = 6f;
    public float auxiliar_1 = 0f;
    public float attackRange = 3f;
    Transform hero;
    Rigidbody2D rb;

    

    Boss boss;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hero = GameObject.FindGameObjectWithTag("Hero").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        boss.LookAtPlayer();
        Vector2 target = new Vector2(hero.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        animator.SetTrigger("perseguir"); 

        if (Vector2.Distance(hero.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("attack1");
            
            Vector2 newPos2 = Vector2.MoveTowards(rb.position, target, auxiliar_1 * Time.fixedDeltaTime);
            rb.MovePosition(newPos2);     
            
        } else if (Vector2.Distance(hero.position, rb.position) >= 7f)
        {
            
            animator.SetTrigger("ataqueDistancia");          
        
        }

        
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("attack1");
       animator.ResetTrigger("perseguir");
       
    }
/*

    IEnumerator WaitForABit()
    {
        yield return new WaitForSeconds(3f);
    }
*/
  
}