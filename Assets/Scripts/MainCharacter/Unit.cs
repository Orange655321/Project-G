using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public virtual void Die() 
    {
        Destroy(gameObject);
    }
    
    public virtual void TakeDamage(int damage) 
    {
        Die();
    }
}
