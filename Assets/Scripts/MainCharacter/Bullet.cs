using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D (Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if(enemy != null) 
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
