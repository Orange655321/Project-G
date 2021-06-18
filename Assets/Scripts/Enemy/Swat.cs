using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swat : AllEnemy
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
    public GameObject prefabAKBullet;
    [SerializeField]
    private GameMaster GM;
    private float AKBulletForce = 9f;
    private float nextAttackTime;
    private float dropChance;
    public ParticleSystem partSys;
    private PistolSoundController pistolSC;
    private Hero hero;
    private bool isInvulnerability;
    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Survival")
        {
            GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMaster>();
        }
        animator = GetComponent<Animator>();
        isInvulnerability = false;
        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("MCblink", typeof(Material)) as Material;
        matDefault = spriteRend.material;
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
        if(player == null || animator == null || pistolSC == null || matBlink == null || matDefault == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            animator = GetComponent<Animator>();
            pistolSC = GetComponent<PistolSoundController>();
            matBlink = Resources.Load("MCblink", typeof(Material)) as Material;
            matDefault = spriteRend.material;
        }
            Angry();
            if (Vector2.Distance(player.transform.position, transform.position) < attackRange)
            {
                animator.SetBool("run", true);
                if (Time.time >= nextAttackTime)
                {
                    nextAttackTime = Time.time + 1f / RateOfFire;
                    Attack();
                }
            }
            else
            {
                animator.SetBool("run", false);
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
        GameObject bullet = Instantiate(prefabAKBullet, pointAttack.transform.position, pointAttack.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Bullet>().isEnemy = true;
        rb.AddForce(transform.up * AKBulletForce, ForceMode2D.Impulse);
        animator.SetTrigger("shoot");
        pistolSC.shootSound();
        partSys.Play();
    }
    public override void TakeDamage(int damage)
    {
        if (!isInvulnerability)
        {
            if (armor == 0)
            {
                health -= damage;
                if (health < 0)
                {
                    health = 0;
                }
            }
            else
            {
                armor -= damage;
                if (armor <= 0)
                {
                    StartCoroutine(Invulnerability());
                    StartCoroutine(Blinking());
                    armor = 0;
                }
            }

            if (health <= 0 && !isDead)
            {
                isDead = true;
                Die();
            }
        }
    }
    public override void Die()
    {
        player.GetComponent<Hero>().AddToScore(cost);
        GameMasterLvl1.EnemyCount--;
        if(dropChance > 0.1)
        {
            GM.spawnItems(transform.position);
        }
        Destroy(gameObject);
    }
    IEnumerator Invulnerability()
    {
        isInvulnerability = true;
        yield return new WaitForSeconds(1.56f);
        isInvulnerability = false;
        yield return null;
    }
    IEnumerator Blinking()
    {
        for (int i = 0; i < 6; ++i)
        {
            //spriteRend.material = matBlink;
            spriteRend.material = matBlink;
            yield return new WaitForSeconds(0.130f);
            spriteRend.material = matDefault;
            yield return new WaitForSeconds(0.130f);
        }
    }
}
