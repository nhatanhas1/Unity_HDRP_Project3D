using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartGameSceneUIManager : MonoBehaviour
{
    [SerializeField] GameObject startMenuPanel;
    [SerializeField] GameObject multiplayerPanel;

    [SerializeField] GameObject connectButton;
    
    [SerializeField] GameObject quickStartButton;
    [SerializeField] GameObject quickCancelButton;
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
        NetWorkingController.instance.OnConnectToMasterServerEvent.AddListener(TurnOnMultiplayerMenu);
    }

    public void ConnectToPhotonServerButton()
    {
        NetWorkingController.instance.ConnectToMasterServerEvent?.Invoke();
        connectButton.SetActive(false);
    }

    void TurnOnMultiplayerMenu()
    {
        startMenuPanel.SetActive(false);
        multiplayerPanel.SetActive(true);
    }

    public void QuickStartButton()
    {
        // Tim va tham gia mot lobby bat ky
        NetWorkingController.instance.QuickStartEvent?.Invoke();
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
    }

    public void QuickCancelButton()
    {
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        NetWorkingController.instance.LeaveRoomEvent?.Invoke();
    }


}
