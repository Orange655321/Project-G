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

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Angry();
    }

    private void Angry() 
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
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
