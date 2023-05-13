using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] LayerMask groundLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool BoxGroundCheck(Vector3 center,float radius,float maxDistance)
    {
        RaycastHit raycastHit;
        Physics.SphereCast(center, radius, Vector3.down, out raycastHit, maxDistance,groundLayerMask);
        //Debug.Log(raycastHit.collider.name);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
            //return true;
        }
        else
        {
            rayColor = Color.red;

            //return false;
        }
        Debug.DrawRay(center, Vector3.down, rayColor);
        //Debug.Log(raycastHit.collider.name);
        return raycastHit.collider != null;
    }
}
