﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{

    public GameObject heroPrefab;
    private void Awake()
    {
        Vector3 pos = new Vector3();

        PhotonNetwork.Instantiate(heroPrefab.name, pos, Quaternion.identity);
    }

    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
   

    public static void leave()
    {
        //убираем героя  с карты
        Debug.Log("WTF");
        PhotonNetwork.LeaveRoom(); 
    }

    public override void OnLeftRoom()
    {
        //когда текущий игрок покинул покидает комнату
        PhotonNetwork.Destroy(heroPrefab);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room ", newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room ", otherPlayer.NickName);
    }
}
