using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnObjectController : MonoBehaviourPun
{
    //[SerializeField] GameObject spawnObject;

    [SerializeField] float spawnTime = 0;
    public float spawnDelay = 3;

    [SerializeField] int maxSpawn;

    // Start is called before the first frame update
    void Start()
    {
        spawnDelay = 3;
        maxSpawn = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) { return; }
        SpawnObject();
    }

    public void SpawnObject() 
    {
        if(maxSpawn < 0) { return; }
        if(Time.time > spawnTime)
        {
            spawnTime = Time.time + spawnDelay;
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Enemy"),this.transform.position,this.transform.rotation);
            maxSpawn--;
        }
    }


}
