using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] private int multiplayerSceneIndex;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        //base.OnEnable();
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
        //base.OnDisable();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined Room");
        StartGame();
        //base.OnJoinedLobby();
    }

    void StartGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting Game");

            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }
}
