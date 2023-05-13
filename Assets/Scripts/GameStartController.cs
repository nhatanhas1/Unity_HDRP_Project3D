using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameStartController : MonoBehaviour
{
    [SerializeField] GameObject spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePlayer()
    {
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PhotonPlayer"),spawnPosition.transform.position, Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnPosition.transform.position, Quaternion.identity);
    }
}
