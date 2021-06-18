using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class GameMasterCoop : MonoBehaviourPunCallbacks
{
    private PhotonView photonView;
    [SerializeField]
    private float minDist;
    [SerializeField]
    private int numberEnemy;
    public static int enemyCount;
    [SerializeField]
    private int enemySpawned;

    [SerializeField]
    private GameObject prefabMedKid;
    [SerializeField]
    private GameObject prefabShieldPack;
    [SerializeField]
    private GameObject prefabPistolBulletPack;
    [SerializeField]
    private GameObject prefabPistol;
    [SerializeField]
    private GameObject prefabAK;
    [SerializeField]
    private GameObject prefabShotgun;
    [SerializeField]
    private GameObject prefabEnemy;

    private GameObject[] respawnPlace;


    private int finalScore;

    private void Awake()
    {
        respawnPlace = GameObject.FindGameObjectsWithTag("Grass");

    }
    void Start()
    {
        photonView = GetComponent<PhotonView>();

    }

    void Update()
    {
        //враги спавняться только в мастерклиенте 
        if (!PhotonNetwork.IsMasterClient) return;
        if (enemySpawned > enemyCount && numberEnemy > 0)
        {
            spawnEnemy();
        }


    }

    private void spawnEnemy()
    {
        float x;
        float y;
        int respawnNumber;
        do
        {
            respawnNumber = Random.Range(0, respawnPlace.Length);
            x = respawnPlace[respawnNumber].transform.position.x;
            y = respawnPlace[respawnNumber].transform.position.y;
        }// выйдем только при false - когда вокруг не будет ни одного префаба 
        while (Physics2D.OverlapCircle(new Vector2(x, y), minDist, prefabEnemy.layer) != null);
        numberEnemy--;
        PhotonNetwork.InstantiateRoomObject(prefabEnemy.name, new Vector3(x, y, transform.position.z), transform.rotation);// собственно, ставим сам префаб 
        enemyCount++;
    }


    public void spawnItems(Vector3 pos)
    {
        ItemsCoop.ItemType item = (ItemsCoop.ItemType)Random.Range(3, 6);
        switch (item)
        {
            case ItemsCoop.ItemType.MedKit:
                PhotonNetwork.InstantiateRoomObject(prefabMedKid.name, pos, prefabMedKid.transform.rotation);
                break;
            case ItemsCoop.ItemType.ShieldPack:
                PhotonNetwork.InstantiateRoomObject(prefabShieldPack.name, pos, prefabShieldPack.transform.rotation);
                break;
            case ItemsCoop.ItemType.PistolBulletPack:
                PhotonNetwork.InstantiateRoomObject(prefabPistolBulletPack.name, pos, prefabPistolBulletPack.transform.rotation);
                break;
            case ItemsCoop.ItemType.Pistol:
                PhotonNetwork.InstantiateRoomObject(prefabPistol.name, pos, prefabPistol.transform.rotation);
                break;
            case ItemsCoop.ItemType.AK:
                PhotonNetwork.InstantiateRoomObject(prefabAK.name, pos, prefabAK.transform.rotation);
                break;
            case ItemsCoop.ItemType.Shotgun:
                PhotonNetwork.InstantiateRoomObject(prefabShotgun.name, pos, prefabShotgun.transform.rotation);
                break;

        }
    }
}