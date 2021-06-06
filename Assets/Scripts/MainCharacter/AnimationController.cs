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
  public void KnifeAnimationOn()
    {
        animator.SetTrigger("knifeOn");
    }

    public void KnifeAnimationOff()
    {
        animator.SetTrigger("knifeOff");
    }

    public void Switcher()
    {
        animator.SetTrigger("switch");
    }

    public void SwitchToPistol()
    {
        animator.SetTrigger("pistol");
    }

    public void SwitchToShotgun()
    {
        animator.SetTrigger("shotgun");
    }

    public void SwitchToAK47()
    {
        animator.SetTrigger("ak-47");
    }
 public void DeathAnimationPlay()
    {
        animator.SetTrigger("death");
    }}
