using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTS : MonoBehaviour
{
    public int health;
    public int damage;
    public int cost;
    public GameObject pointAttack;
    public GameObject prefabAKBullet;
    public GameObject prefabBobs;
    private float AKBulletForce = 10f;
    public ParticleSystem partSys;
    private PistolSoundController pistolSC;
    public GameObject player;
    private float nextAttackTime;
    public float RateOfFire;
    public float attackRange;
    private bool isDead;
    void Start()
    {
        pistolSC = GetComponent<PistolSoundController>();
        nextAttackTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.transform.position, transform.position) < attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + RateOfFire;
                Attack();
            }
        }
    }

    private void Attack()
    {
        GameObject bullet = Instantiate(prefabAKBullet, pointAttack.transform.position, pointAttack.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Bullet>().isEnemy = true;
        rb.AddForce(transform.up * AKBulletForce, ForceMode2D.Impulse);
        pistolSC.shootSound();
        partSys.Play();
    }

    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }
    void Die()
    {
        player.GetComponent<Hero>().AddToScore(cost);
       
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
