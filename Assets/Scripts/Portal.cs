using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public enum PortalSide
    {
        left,
        right       
    }
    public PortalSide side;
    public Transform leftPortalPos;
    public Transform rightPortalPos;
    private Vector3 posOffset = new Vector3(2f,0f,0f);
    private void OnTriggerEnter(Collider other)
    {

        other.gameObject.transform.position = side == PortalSide.left ? new Vector3(rightPortalPos.position.x + 4,0f,34f) : new Vector3(leftPortalPos.position.x - 4,0,34);        
    }
}
