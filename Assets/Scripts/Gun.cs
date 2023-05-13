using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class Gun : MonoBehaviourPun , IPunInstantiateMagicCallback
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gunBarrel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(PlayerController player)
    {
        Debug.Log("Setup gun owner");
        //this.gameObject.transform.SetParent(player.transform.GetComponent<WeaponManager>().weaponHolder);
    }

    private void OnEnable()
    {
        //PhotonView.Find(photonView.ViewID);
        Debug.Log("ViewID " + photonView.Controller);
        
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;

        this.gameObject.transform.SetParent(PhotonView.Find((int)instantiationData[0]).GetComponent<WeaponManager>().weaponHolder);
        Debug.Log("Test  ddds  " + instantiationData[0].ToString()); 
    }

    public void Shoot()
    {
        //Debug.Log("Instantiate bullet");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), gunBarrel.transform.position,gunBarrel.transform.rotation);
    }
}
