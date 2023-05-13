using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviour
{
    PhotonView photonView;

    public Transform target;

    public Vector3 offset;
    private void Awake()
    {
        //photonView = photonView = GetComponentInParent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!photonView.IsMine) { return; }
        if (target == null) { return; }
        transform.position = target.position + offset;
    }

    public void SetupTarget(PlayerController cameraTarget)
    {
        if(target !=null)
        {
            target = null;
        }

        target = cameraTarget.transform;
    }

}
