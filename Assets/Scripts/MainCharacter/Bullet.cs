using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D (Collider2D collider)
    {
       
        Enemy enemy = collider.GetComponent<Enemy>();
        Hero hero = collider.GetComponent<Hero>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(hero != null)
        {
            hero.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collider.CompareTag("Wall") || collider.CompareTag("Gate"))
        {
            Destroy(gameObject);
        }
    }
}
