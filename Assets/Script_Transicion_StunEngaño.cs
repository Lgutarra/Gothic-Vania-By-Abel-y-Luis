using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Transicion_StunEngaño : StateMachineBehaviour
{
    Transform hero;
    Rigidbody2D rb;
    Boss boss;
    
    
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hero = GameObject.FindGameObjectWithTag("Hero").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        animator.ResetTrigger("perseguir"); 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        animator.SetTrigger("EngañarStun");     
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetTrigger("EngañarStun");  
    }



    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Implement code that processes and affects root motion
       animator.ResetTrigger("ataqueDistancia"); 
       
    }



}
