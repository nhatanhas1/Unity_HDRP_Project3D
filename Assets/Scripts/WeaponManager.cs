using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
    
    public Transform weaponHolder;
    [SerializeField] Gun startingGun;
    public GameObject currentGun;
    Gun equippedGun;

    private void Awake()
    {
        photonView = GetComponentInParent<PhotonView>();
    }
    void Start()
    {
        if (!photonView.IsMine) { return; }
        if (startingGun != null)
        {
            EquipWeapon(startingGun);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) { return; }
        if(Input.GetKeyUp(KeyCode.Space)) 
        {
            Test();
        }
    }

    public void EquipWeapon(Gun gunToEquip)
    {

        if(equippedGun != null)
        {
            Destroy(equippedGun);
        }
        //equippedGun = Instantiate(gunToEquip, weaponHolder.position, weaponHolder.rotation) as Gun;

        if (equippedGun == null)
        {
            object[] myCustomInitData = GetInitData();
            //currentGun = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Gun"), weaponHolder.position, weaponHolder.rotation);
            currentGun = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Gun"), weaponHolder.position, weaponHolder.rotation,0,myCustomInitData);
        }

        photonView.RPC("RPC_SetOwner", RpcTarget.All, currentGun.GetComponent<PhotonView>().ViewID, photonView.ViewID);
        //equippedGun = test.GetComponent<Gun>();
        //equippedGun.gameObject.transform.parent = weaponHolder.transform;
    }

    object[] GetInitData()
    {
        object[] data = new object[1];
        data[0] = photonView.ViewID;
        return data;
    }

    [PunRPC]
    void RPC_SetOwner(int itemID,int playerPhotonViewID)
    {
        Debug.Log("Call RPC + " + photonView.ViewID + " PLayer Owner : " + playerPhotonViewID + " ItemID "+ itemID);        
        
        PhotonView.Find(itemID).gameObject.transform.SetParent(PhotonView.Find(playerPhotonViewID).GetComponent<WeaponManager>().weaponHolder);

        //gun.gameObject.transform.SetParent(weaponHolder.transform);
    }

    void Test()
    {
        
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Gun"), weaponHolder.position, weaponHolder.rotation);
    }
}
