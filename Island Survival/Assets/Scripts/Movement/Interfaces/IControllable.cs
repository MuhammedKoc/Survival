using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    public Vector2 GetMoveDirection();

    public bool GetRunBool();

    public Vector2 GetLastDirection();
}
