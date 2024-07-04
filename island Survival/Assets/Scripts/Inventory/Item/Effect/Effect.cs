using System.Collections;
using System.Collections.Generic;
using Inventory.Item;
using UnityEngine;

public class Effect : ScriptableObject
{
    public string Name;

    public Sprite Icon;

    [HideInInspector]
    public int value;

    public virtual void Use(EffectChangeType changeType)
    {

    }
}
