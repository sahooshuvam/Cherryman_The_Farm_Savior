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

    public void Idle()
    {
        animator.SetTrigger("IsIdle");
    }

    public void Walk()
    {
        animator.SetTrigger("IsWalking");
    }

    public void Run()
    {
        animator.SetTrigger("IsRunning");
    }

    public void Jump()
    {
        animator.SetTrigger("IsJumping");
    }

    public void Attack()
    {
        animator.SetTrigger("IsThrowing");
    }

    public void CollectingCherry()
    {
        animator.SetTrigger("IsCollecting");
    }

    public void Die()
    {
        animator.SetBool("Die",true);
    }
}
