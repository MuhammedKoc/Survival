using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodObject", menuName = "Inventory/New Food Object")]
public class FoodObject : ItemObject
{
    public int GiveHeal;

    private void Awake()
    {
        type = ItemType.Food;
    }
}
