using System.Collections;
using System.Collections.Generic;
using Inventory.Item;
using UnityEngine;
using UnityEngine.Localization;

public class Effect : ScriptableObject
{
    public LocalizedString localizedName;

    public Sprite Icon;

    [HideInInspector]
    public int value;

    public virtual void Use(EffectChangeType changeType)
    {

    }
}
