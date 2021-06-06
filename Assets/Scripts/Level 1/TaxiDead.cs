using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiDead : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(1337);
        }
    }
}
