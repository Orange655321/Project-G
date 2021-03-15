using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int armor;
    public int RateOfFire;
    public int speed;

    
    private Transform player;
    private Rigidbody2D rb;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Angry();
    }

    private void Angry() 
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
        Vector2 lookDir = player.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //угол между вектором от объекта и героем
        rb.rotation = angle;// привязка угла к герою
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
        Destroy(gameObject);
    }
}
