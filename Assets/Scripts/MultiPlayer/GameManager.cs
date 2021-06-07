using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject heroPrefab;
    void Start()
    {
        Vector3 pos = new Vector3(Random.RandomRange(-5f, 5f), Random.RandomRange(-5f, 5f));

        PhotonNetwork.Instantiate(heroPrefab.name, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void leave()
    {
        PhotonNetwork.LeaveRoom(); 
    }

    public override void OnLeftRoom()
    {
        //когда текущий игрок покинул покидает комнату
        SceneManager.LoadScene(0);
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
