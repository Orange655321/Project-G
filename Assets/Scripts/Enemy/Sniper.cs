using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sniper : AllEnemy
{
    public int health;
    public int armor;
    public float RateOfFire;
    public int speed;
    public int cost;
    public int damage;
    public float attackRange;
    public GameObject pointAttack;
    public LayerMask layerHero;

    private bool isDead;
    private GameObject player;
    public Rigidbody2D rb;
    [SerializeField]
    private GameMaster GM;
    private float nextAttackTime;
    private float dropChance;
    private Hero hero;
    private AnimatorControlerEnemy animCtrl;
    public GameObject line;
    private PistolSoundController pistolSC;
    // Start is called before the first frame update
    void Start()
    {
        line.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Survival")
        {
            GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMaster>();
        }
        pistolSC = GetComponent<PistolSoundController>();
        animCtrl = GetComponent<AnimatorControlerEnemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        nextAttackTime = 0f;
        dropChance = Random.Range(0f, 1f);
        isDead = false;
        hero = player.GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        Angry();
            if (Vector2.Distance(player.transform.position, transform.position) < attackRange)
                if (Time.time >= nextAttackTime && !line.activeSelf)
                    StartCoroutine(ShotDelay());
    }
    public override void Angry()
    {
        Vector2 lookDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //угол между вектором от объекта и героем
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public override void Attack()
    {
        RaycastHit2D[] raycasts = Physics2D.RaycastAll(pointAttack.transform.position, transform.up);
        foreach (RaycastHit2D her in raycasts)
        {
            if (her.collider.CompareTag("Wall") || her.collider.CompareTag("Gate"))
            {
                return;
            }
            if (her.collider.CompareTag("Player"))
            {
                 her.collider.GetComponent<Hero>().TakeDamage(damage); 
            }
        }
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
        if (dropChance > 0.1)
        {
            GM.spawnItems(transform.position);
        }
        Destroy(gameObject);
    }
    IEnumerator ShotDelay()
    {
        line.SetActive(true);
        yield return new WaitForSeconds(2f);
        nextAttackTime = Time.time + RateOfFire;
        Attack();
        line.SetActive(false);
        pistolSC.shootSound();
        yield return null;
    }
}
