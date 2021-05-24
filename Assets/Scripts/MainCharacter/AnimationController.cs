using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void RunAnimationOn()
    {
        animator.SetBool("isRunning", true);
    }
    public void RunAnimationOff()
    {
        animator.SetBool("isRunning", false);
    }

    public void ShootAnimationPlay()
    {
        animator.SetTrigger("shoot");
    }
    public void DeathAnimationPlay()
    {
        animator.SetTrigger("death");
    }
}
