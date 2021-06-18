using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyCoop : MonoBehaviourPunCallbacks
{
    public PhotonView photonView;
    public int health;
    public int armor;
    public int RateOfFire;
    public int speed;
    public int cost;
    public int zombieDamage;
    public float attackRange;
    public GameObject pointAttack;//сделай по уму, с без геймобжекта
    public LayerMask layerHero;

    private bool isDead;
    private GameObject player;
    private GameMasterCoop GM;
    public Rigidbody2D rb;
    private float nextAttackTime;
    private float dropChance;
    private List<Vector2> pathToPlayer;
    AstarPathFinder pathFinder;
    // private AstarPathFinder pathFinder;
    private bool isMoving;
    public void Start()
    {
        photonView = GetComponent<PhotonView>();

        player = GameObject.FindGameObjectWithTag("Player");
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMasterCoop>();
        rb = GetComponent<Rigidbody2D>();
        pathFinder = GetComponent<AstarPathFinder>();
        //pathFinder = GetComponent<AstarPathFinder>();
        nextAttackTime = 0f;
        dropChance = Random.Range(0f, 1f);
        isDead = false;
    }


    private void Update()
    {
        if (!photonView.IsMine) return;
        if (health <= 0)
        {
            isDead = true; 
            Die();
        }
        if (player != null)
        {

            if (Vector2.Distance(player.transform.position, transform.position) > attackRange)
            {
                Angry();
            }
            else
            {
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / RateOfFire;
                }
            }
        }
    }
    private void FixedUpdate()
    {

    }

    private void Attack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(pointAttack.transform.position, attackRange / 3, layerHero);
        if (colInfo != null)
        {
            player.GetComponent<HeroCoop>().TakeDamage(zombieDamage);
        }
    }
    private void Angry()
    {
        Vector2 lookDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //угол между вектором от объекта и героем
        transform.eulerAngles = new Vector3(0, 0, angle);
        rb.velocity = lookDir.normalized * speed;
      
    }
    [PunRPC]
    public void TakeDamage(int damage)
    {
        health -= damage;
        
       
    }
    void Die()
    {   
        player.GetComponent<HeroCoop>().AddToScore(cost);
        if(dropChance > 0.5)
        {
            GM.spawnItems(transform.position);
        }
        PhotonNetwork.Destroy(gameObject);
    }
    

}
