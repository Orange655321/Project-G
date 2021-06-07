using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletCoop : MonoBehaviourPunCallbacks
{
    public int damage;
    private void OnTriggerEnter2D (Collider2D collider)
    {    
        EnemyCoop enemy = collider.GetComponent<EnemyCoop>();
        if(enemy != null) 
        {
            enemy.TakeDamage(damage);
            PhotonNetwork.Destroy(gameObject);
        }
        if (collider.CompareTag("Wall") || collider.CompareTag("Gate"))
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
