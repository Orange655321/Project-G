﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : AllEnemy
{
    public int health;
    public int armor;
    public int RateOfFire;
    public int speed;
    public int cost;
    public int damage;
    public float attackRange;
    public GameObject pointAttack;
    public LayerMask layerHero;

    private bool isDead;
    private GameObject player;
    public Rigidbody2D rb;
    public GameObject prefabPistolBullet;
    [SerializeField]
    private GameMaster GM;
    private float pistolBulletForce = 8f;
    private float nextAttackTime;
    private float dropChance;
    public ParticleSystem partSys;
    private PistolSoundController pistolSC;
    private AnimatorControlerEnemy animCtrl;
    private Hero hero;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMaster>();
        animCtrl = GetComponent<AnimatorControlerEnemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        pistolSC = GetComponent<PistolSoundController>();
        rb = GetComponent<Rigidbody2D>();
        nextAttackTime = 0f;
        dropChance = Random.Range(0f, 1f);
        isDead = false;
        hero = player.GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hero.isDie())
        {
            Angry();
            if (Vector2.Distance(player.transform.position, transform.position) < attackRange)
            {
                animCtrl.RunAnimationOff();
                if (Time.time >= nextAttackTime)
                {
                    nextAttackTime = Time.time + 1f / RateOfFire;
                    Attack();
                }
            }
            else
            {
                animCtrl.RunAnimationOn();
            }
        }
    }
    public override void Angry()
    {
        Vector2 lookDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //угол между вектором от объекта и героем
        transform.eulerAngles = new Vector3(0, 0, angle);
    }


    public override void Attack()
    {
        GameObject bullet = Instantiate(prefabPistolBullet, pointAttack.transform.position, pointAttack.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Bullet>().isEnemy = true;
        rb.AddForce(transform.up * pistolBulletForce, ForceMode2D.Impulse);
        animCtrl.PlayAnimationShoot();
        pistolSC.shootSound();
        partSys.Play();
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
        if(dropChance > 0.5)
        {
            GM.spawnItems(transform.position);
        }
        Destroy(gameObject);
    }
}
