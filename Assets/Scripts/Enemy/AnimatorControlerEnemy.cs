using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControlerEnemy : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void AttackAnimationOn()
    {
        animator.SetBool("isAttacking", true);
    }
    public void AttackAnimationOff()
    {
        animator.SetBool("isAttacking", false);
    }
    public void RunAnimationOn()
    {
        animator.SetBool("run", true);
    }
    public void RunAnimationOff()
    {
        animator.SetBool("run", false);
    }
    public void PlayAnimationShoot()
    {
        animator.SetTrigger("shoot");
    }
    public void GetKnifeOn()
    {
        animator.SetBool("knife", true);
    }
    public void GetKnifeOff()
    {
        animator.SetBool("knife", false);
    }
}
