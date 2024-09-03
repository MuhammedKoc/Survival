using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

[RequireTag("FadeableObject")]
public class FadeableObject : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer fadeObject;

    public SpriteRenderer FadeObject => fadeObject;
}

