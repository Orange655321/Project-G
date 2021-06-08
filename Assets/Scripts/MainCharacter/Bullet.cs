using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isEnemy;
    private void OnTriggerEnter2D (Collider2D collider)
    {
        if (!isEnemy)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            Swat swat = collider.GetComponent<Swat>();
            Police police = collider.GetComponent<Police>();
            B b = collider.GetComponent<B>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (swat != null)
            {
                swat.TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (police != null)
            {
                police.TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (b != null)
            {
                b.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Hero hero = collider.GetComponent<Hero>();
            if (hero != null)
            {
                hero.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        if (collider.CompareTag("Wall") || collider.CompareTag("Gate"))
        {
            Destroy(gameObject);
        }
    }
}
