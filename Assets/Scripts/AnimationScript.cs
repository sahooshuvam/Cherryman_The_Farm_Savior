using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Idle() // this method is for Cherryman Idle Animation 
    {
        animator.SetTrigger("IsIdle");
    }

    public void Walk()
    {
        animator.SetTrigger("IsWalking"); // this method is for Cherryman Walk Animation 
    }

    public void Run()
    {
        animator.SetTrigger("IsRunning"); // this method is for Cherryman Run Animation 
    }

    public void Jump() // this method is for Cherryman Jump Animation 
    {
        Debug.Log("Before Trigger");
        animator.SetTrigger("IsJumping");
        Debug.Log("After Trigger");

    }

    public void Attack() // this method is for Cherryman Attack Animation 
    {
        animator.SetTrigger("IsThrowing");
    }

    public void CollectingCherry() // this method is for Cherryman Collecting Cherry Animation 
    {
        animator.SetTrigger("IsCollecting");
    }

    public void Die() // this method is for Cherryman Die Animation 
    {
        animator.SetBool("IsDie",true);
    }
}
