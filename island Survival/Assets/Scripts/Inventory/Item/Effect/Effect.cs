using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New  Effect", menuName ="Inventory/Effect/New Effect")]
public class Effect : ScriptableObject
{
    public string Name;

    public Sprite Icon;

    [HideInInspector]
    public int value;

    public FloatVariable variable;

    public virtual void Use()
    {

    }
}
