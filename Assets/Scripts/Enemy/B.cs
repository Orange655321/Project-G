using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B : AllEnemy
{
    public int health;
    public int RateOfFire;
    public int speed;
    public int cost;
    public int BDamage;
    public float attackRange;
    public GameObject pointAttack;
    public LayerMask layerHero;

    private bool isDead;
    private GameObject player;
    private GameMaster GM;
    public Rigidbody2D rb;
    private float nextAttackTime;
    private float dropChance;
    private bool isMoving;
    private Hero hero;
    private AnimatorControlerEnemy animCtrl;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animCtrl = GetComponent<AnimatorControlerEnemy>();
        //GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMaster>();
        rb = GetComponent<Rigidbody2D>();
        nextAttackTime = 0f;
        dropChance = Random.Range(0f, 1f);
        isDead = false;
        hero = player.GetComponent<Hero>();
    }


    private void Update()
    {
        if (hero.isDie())
        {

            if (Vector2.Distance(player.transform.position, transform.position) > attackRange)
            {
                animCtrl.GetKnifeOff();
                animCtrl.RunAnimationOn();
                Angry();
            }
            else
            {
                if (Time.time >= nextAttackTime)
                {
                    animCtrl.RunAnimationOff();
                    Attack();
                    nextAttackTime = Time.time + 1f / RateOfFire;
                }
            }
        }
    }

    public override void Attack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(pointAttack.transform.position, attackRange / 3, layerHero);
        if (colInfo != null)
        {
            animCtrl.GetKnifeOn();
            player.GetComponent<Hero>().TakeDamage(BDamage);
        }
    }
    public override void Angry()
    {
        Vector2 lookDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //угол между вектором от объекта и героем
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }
    public override void Die()
    {
        player.GetComponent<Hero>().AddToScore(cost);
        GameMasterLvl1.EnemyCount--;
        /*if(dropChance > 0.5)
        {
            GM.spawnItems(transform.position);
        }*/
        Destroy(gameObject);
    }
}
