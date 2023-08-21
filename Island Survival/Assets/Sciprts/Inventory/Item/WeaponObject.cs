using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapýn",menuName ="Inventory/New Weapon Object")]
public class WeaponObject : ItemObject
{
    public WeaponType weaponType;

    private void Awake()
    {
        type = ItemType.Weapon;
    }
}

public enum WeaponType
{
    Sword,
    Spear,
    Bow,
    Katana,
    dagger
}
