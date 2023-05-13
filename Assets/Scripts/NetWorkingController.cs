using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;

public class NetWorkingController : MonoBehaviourPunCallbacks
{
    public static NetWorkingController instance;
    
    [SerializeField] int maxRoomSize;

    public UnityEvent ConnectToMasterServerEvent;
    public UnityEvent OnConnectToMasterServerEvent;

    public UnityEvent QuickStartEvent;
    public UnityEvent LeaveRoomEvent;

    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Setup()
    {
        ConnectToMasterServerEvent.AddListener(ConnectToPhotonServer);
        QuickStartEvent.AddListener(QuickStart);
        LeaveRoomEvent.AddListener(LeaveRoom);
    }

    void ConnectToPhotonServer()
    {
        PhotonNetwork.ConnectUsingSettings();        
    }


    //Funsition overrtide tu MonoBehaviourPunCallBack
    public override void OnConnectedToMaster()
    {   
        PhotonNetwork.AutomaticallySyncScene = true;
        OnConnectToMasterServerEvent?.Invoke();
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server.");
    }

    // Tim va tham gia mot lobby bat ky
    public void QuickStart()
    {        
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Join  random room");
    }

    //Khi join random lobby that bai
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join random room");
        CreateRoom();
    }

    void CreateRoom()
    {        
        string systemTime = System.DateTime.Now.ToString();
        RoomOptions roomOps = new RoomOptions() { IsVisible = true,IsOpen = true, MaxPlayers = (byte)maxRoomSize};
        PhotonNetwork.CreateRoom("RoomID:" + systemTime, roomOps);
        Debug.Log("Create Room! " +"RoomID: " +systemTime );
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Failed to  create room... trying again ");
        CreateRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Leave Room! ");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
    }
}
