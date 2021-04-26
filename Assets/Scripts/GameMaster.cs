using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
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
    private GameObject prefabEnemy;

    private GameObject[] respawnPlace;

    private void Awake()
    {
    }
    void Start()
    {
        respawnPlace = GameObject.FindGameObjectsWithTag("Grass");
    }

    void Update()
    {
        if(enemySpawned > enemyCount && numberEnemy > 0)
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
        Instantiate(prefabEnemy, new Vector3(x, y, transform.position.z), transform.rotation);// собственно, ставим сам префаб
        enemyCount++;
    }

    public void spawnItems(Vector3 pos)
    {
        Items.ItemType item = (Items.ItemType)Random.Range(0, 3);
        switch (item)
        {
            case Items.ItemType.MedKit:
                Instantiate(prefabMedKid, pos, prefabMedKid.transform.rotation);
                break;
            case Items.ItemType.ShieldPack:
                Instantiate(prefabShieldPack, pos, prefabShieldPack.transform.rotation);
                break;
            case Items.ItemType.PistolBulletPack:
                Instantiate(prefabPistolBulletPack, pos, prefabPistolBulletPack.transform.rotation);              
                break;
        }
    }
}
