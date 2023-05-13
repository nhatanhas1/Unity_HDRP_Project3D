using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyController>() != null)
        {
            other.GetComponent<EnemyController>().target = this.gameObject.transform.parent.transform;
            Debug.Log("Set enemy Target");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            other.GetComponent<EnemyController>().target = null;
        }
    }
}
