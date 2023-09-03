using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    private void Awake()
    {
        // Bir �rnek varsa ve ben de�ilse, yoket.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
