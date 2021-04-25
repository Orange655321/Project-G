using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int armor;
    public int RateOfFire;
    public int speed;
    public int cost;
    public int zombieDamage;
    public float attackRange;
    public GameObject pointAttack;//сделай по уму, с без геймобжекта
    public LayerMask layerHero;


    private GameObject player;
    private Rigidbody2D rb;
    private float nextAttackTime;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        nextAttackTime = 0f;
    }
 

    private void Update()
    { 
        if(player != null) 
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
        Collider2D colInfo = Physics2D.OverlapCircle(pointAttack.transform.position, attackRange/3, layerHero);
        if (colInfo != null)
        {
            player.GetComponent<Hero>().TakeDamage(zombieDamage);
        }
    }
    private void Angry() 
    {
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
        Vector2 lookDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //угол между вектором от объекта и героем
        transform.eulerAngles = new Vector3(0, 0, angle);
        rb.velocity = lookDir.normalized * speed;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0) 
        {
            Die();
        }
    }
    void Die()
    {
        player.GetComponent<Hero>().AddToScore(cost);
        Destroy(gameObject);
    }

   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) 
        {
            isAttack = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            isAttack = false;
        }
    }*/

    private IEnumerator CoroutineAttack()
    {
        yield return new WaitForSeconds(1);
        
        yield break;
    }
}
