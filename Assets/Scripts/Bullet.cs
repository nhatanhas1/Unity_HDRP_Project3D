using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    [SerializeField] float speed = 10;
    [SerializeField] float timer = 0;
    [SerializeField] float durationTime = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) { return; }
        if (timer > durationTime)
        {
            timer = 0;
            PhotonNetwork.Destroy(gameObject);
            //Debug.Log("Destroy bullet");
        }
        else
        {
            timer += Time.deltaTime;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }

    //private void OnEnable()
    //{

    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IIDamageable>() != null)
        {
            IIDamageable damageable = other.GetComponent<IIDamageable>();
            damageable.TakeDamage(10);
        }
    }
}
