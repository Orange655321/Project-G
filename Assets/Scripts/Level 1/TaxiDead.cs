using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiDead : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        B enemy = collision.collider.GetComponent<B>();
        if (enemy != null)
        {
            enemy.TakeDamage(1337);
        }
    }
}
