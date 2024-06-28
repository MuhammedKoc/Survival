using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    [SerializeField] private LayerMask Mask;
    [SerializeField] private float LedgeDetectorSize;
    [SerializeField] private Vector2 LedgeDetectorOffset;
    RaycastHit2D hit;


    private void Update()
    {
        LedgeDetect();
    }

    private void LedgeDetect()
    {
        hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up));   
        if (Physics2D.OverlapCircle((Vector2)transform.position + LedgeDetectorOffset, LedgeDetectorSize,Mask))
        {
           
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,hit.point);
    }
}
