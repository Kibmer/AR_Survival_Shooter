using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonConnectManager : Photon.PunBehaviour
{
    public int SceneIndexToMove = 1;
    public int MaxPlayerCountInRoom = 4;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings("1");
    }
    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg){
        RoomOptions op = new RoomOptions();
        op.MaxPlayers = (byte)MaxPlayerCountInRoom;
        PhotonNetwork.CreateRoom(null, op, null);
    }
    public override void OnJoinedRoom(){
        PhotonNetwork.LoadLevel(SceneIndexToMove);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
