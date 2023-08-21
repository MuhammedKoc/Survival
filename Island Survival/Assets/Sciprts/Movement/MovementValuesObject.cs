using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Movement Value", menuName = "Movement/New Movement Value")] 
public class MovementValuesObject : ScriptableObject
{
    public float Speed;

    [Header("Run")]
    public float RunSpeed;
    public float RunStaminaCost;

    [Header("Dash")]
    public float DashSpeed;
    public float DashDuration;
    public float DashStaminaCost;
}
