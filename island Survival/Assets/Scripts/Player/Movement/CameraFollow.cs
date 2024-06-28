using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] Vector3 Offset;
    [SerializeField] float Damping;

    private Vector3 velocity = Vector3.zero;
    public void FixedUpdate()
    {
        Vector3 movePosition = Target.position + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, Damping);

    }
}
