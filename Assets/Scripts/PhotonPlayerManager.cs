using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonPlayerManager : MonoBehaviour
{
    //[SerializeField] PlayerController playerController;
    PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if(!photonView.IsMine && GetComponentInChildren<PlayerController>() != null)
        {
            Destroy(GetComponentInChildren<PlayerController>());
        }
    }
}
