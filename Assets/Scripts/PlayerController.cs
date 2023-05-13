using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun , IIDamageable
{
    [SerializeField] PhotonView photonView;
    [SerializeField] Rigidbody rb;
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] GroundCheck groundCheck;

    [SerializeField] WeaponManager weaponManager;
    //[SerializeField] Gun startingGun;
    [SerializeField] Camera camera;
    [SerializeField] Vector3 lookAtPoint;

    Vector3 moveDir;
    [SerializeField] float moveSpeed;

    


    bool isMove;

    bool isDead;
    public int hitPoint;
    public int currentHitPoint;

    public int armor;
    public int attackDamage;
    int finalDamageTake;


    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>(); 
        playerCollider = GetComponent<CapsuleCollider>();

        groundCheck = GetComponent<GroundCheck>();

        weaponManager = GetComponentInParent<WeaponManager>();
        
    }
    void Start()
    {
        //Destroy(camera.gameObject);
        if (!photonView.IsMine) 
        {
            Destroy(rb);
            //Destroy(lookAtPoint);
            //Destroy(camera.gameObject);
            return; 
        }
        GetCamera();


        //weaponManager.EquipWeapon(startingGun);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) { return; }
        GetPlayerMovemantInput();
        GetPlayerLookAtInput();
        GetShootInput();
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) { return; }
        Movemeant();
    }

    void CheckState()
    {
        if (groundCheck.BoxGroundCheck(this.transform.position + playerCollider.center, playerCollider.radius,playerCollider.height/2 + 0.1f))
        {
            //if(rb.isKinematic ==false)
            //rb.isKinematic = true;
            Debug.Log(playerCollider.center);
            Debug.Log("Grounded");
            
        }
        else
        {
            //rb.isKinematic = false;
            Debug.Log("Not on ground");
        }

    }

    void GetPlayerMovemantInput()
    {
        //if (!photonView.IsMine) { return; }
        CheckState();

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.z = Input.GetAxisRaw("Vertical");
            isMove = true;
        }
        else
        {
            if (isMove)
            {
                moveDir = Vector3.zero;     
            }
        }               
    }

    void Movemeant()
    {
        //if(!photonView.IsMine) { return; }
        if (moveDir.magnitude !=0)
        {
            rb.velocity = moveDir.normalized * moveSpeed;
        }
        else
        {
            if (isMove)
            {
                //Debug.Log("rb.velocity = 0");
                rb.velocity = Vector3.zero;
                isMove=false;
            }
            
        }
    }


    void GetPlayerLookAtInput()
    {
        //if (camera == null)
        //{
        //    GetCamera();
        //}


        //Ray ray = camera.ScreenPointToRay(Input.mousePosition);        
        //Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //float rayDistance;
        //if (groundPlane.Raycast(ray, out rayDistance))
        //{
        //    Vector3 point = ray.GetPoint(rayDistance);
        //    Debug.DrawLine(ray.origin, point, Color.red);

        //    LookAt(point);
        //}

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            LookAt(hit.point);
        }
    }

    void LookAt(Vector3 lookPoint)
    {
        //if (!photonView.IsMine) { return; }
        Vector3 hightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(hightCorrectedPoint);
        lookAtPoint = hightCorrectedPoint;
    }

    //Truyen du lieu di chuyen bang code
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //    }else if(stream.IsReading)
    //    {
    //        transform.position = (Vector3)stream.ReceiveNext();
    //        transform.rotation = (Quaternion)stream.ReceiveNext();
    //    }
    //}

    void GetCamera()
    {
        if (camera != null)
        {
            camera = null;
        }
        camera = FindObjectOfType<CameraController>().gameObject.GetComponent<Camera>();
        camera.GetComponent<CameraController>().SetupTarget(this);
    }

    void GetShootInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("PlayerId: " + photonView.ViewID +  " Shoot");
            weaponManager.currentGun.GetComponent<Gun>().Shoot();
        }
        
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            //Debug.Log("Enemy Take Damage");
            finalDamageTake = damage > armor ? (damage - armor) : 0;
            currentHitPoint -= finalDamageTake;
            //PopupDamage();
            if (currentHitPoint <= 0)
            {
                Dead();
            }
        }

    }



    void Dead()
    {
        if (isDead) { return; }
        isDead = true;
        Debug.Log("Dead");
    }
}


public interface IIDamageable
{
    void TakeDamage(int damage);
}