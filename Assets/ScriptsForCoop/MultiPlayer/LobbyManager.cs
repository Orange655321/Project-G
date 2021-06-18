using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon.StructWrapping;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public const string MAP_PROP_KEY = "map";

    public static int keyOfMap;
    void Start()
    {
        PhotonNetwork.NickName = "Player " + Random.Range(1, 100);
        Log("Player's nickname is set to " + PhotonNetwork.NickName);
        PhotonNetwork.GameVersion = "alpha v 1.0.0";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Log("Connected to master");
    }
    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomPropertiesForLobby = new string[] { MAP_PROP_KEY };
        roomOptions.CustomRoomProperties = new Hashtable { { MAP_PROP_KEY, Random.Range(0,100) }};
        PhotonNetwork.CreateRoom(null, roomOptions, null);
        //roomOptions.CustomRoomPropertiesForLobby = { SEED_OF_MAP, AI_KEY, HZ_KEY};
        //roomOptions.CustomRoomProperties = new Hashtable { { SEED_OF_MAP, Random.Range(0, 100) } };
        //PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2});
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        int a = 0;
        bool d = PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MAP_PROP_KEY, out a);
        keyOfMap = a;
    }
    public override void OnJoinedRoom()
    {
        

        Log("Joined the room");
        PhotonNetwork.LoadLevel("COOP");
    }
    public void leave() {
        PhotonNetwork.LeaveRoom();

    }

    private void Log(string message)
    {
        Debug.Log(message);
 
    }
}
