using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singelton<T> : MonoBehaviour
    where T : class, new()
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {

            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this as T;
    }
}


