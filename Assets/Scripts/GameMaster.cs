﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject prefabPistol;
    [SerializeField]
    private GameObject prefabAK;
    [SerializeField]
    private GameObject prefabShotgun;
    [SerializeField]
    private GameObject prefabEnemy;

    private GameObject[] respawnPlace;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text waveText;
    [SerializeField]
    private Text ememiesText;

    private int finalScore;
    private int hours;
    private int minutes;
    private int seconds;
    private float timeGame;
    private void Awake()
    {
    }
    void Start()
    {
        respawnPlace = GameObject.FindGameObjectsWithTag("Grass");
        waveText.text = "" + 1;
        seconds = 0;
        minutes = 0;
        hours = 0;
    }

    void Update()
    {
        if(enemySpawned > enemyCount && numberEnemy > 0)
        {
            spawnEnemy();
        }
        timeGame += Time.deltaTime;
        if(timeGame > 1)
        {
            seconds++;
            timeGame = 0;
        }
        if(seconds > 59)
        {
            seconds = 0;
            minutes++; 
        }
        if(minutes > 59)
        {
            seconds = 0;
            minutes = 0;
            hours++;
        }
        timeText.text = hours + ":" + minutes + ":" + seconds;

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

    public void setScore(int heroScore)
    {
        finalScore = heroScore;
        PlayerDataHolder.score = finalScore;
    }
    public void spawnItems(Vector3 pos)
    {
        Items.ItemType item = (Items.ItemType)Random.Range(3, 6);
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
            case Items.ItemType.Pistol:
                Instantiate(prefabPistol, pos, prefabPistol.transform.rotation);
                break;
            case Items.ItemType.AK:
                Instantiate(prefabAK, pos, prefabAK.transform.rotation);
                break;
            case Items.ItemType.Shotgun:
                Instantiate(prefabShotgun, pos, prefabShotgun.transform.rotation);
                break;

        }
    }
}
