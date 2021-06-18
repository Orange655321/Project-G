using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletCoop : MonoBehaviourPunCallbacks
{
    public int damage;
    // private PhotonView photonView;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //PhotonView photonView = collider.GetComponent<PhotonView>();
        EnemyCoop enemy = collider.GetComponent<EnemyCoop>();

        if (enemy != null)
        {
            PhotonView photonView = enemy.photonView;
            if (!photonView.IsMine) photonView.RPC("TakeDamage", RpcTarget.MasterClient, damage);
            else enemy.TakeDamage(damage);

            PhotonNetwork.Destroy(gameObject);
        }
        if (collider.CompareTag("Wall") || collider.CompareTag("Gate"))
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}