using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : PooledObject
{
    
    public ItemObject item;
    [Tooltip("Non-stackable items cannot have more than 1 amount")]
    public int Amount;

    public bool isMagnetable = false;

    [SerializeField]
    private SpriteRenderer sprite;

    public void Init(ItemObject item, int amount)
    {
        this.item = item;
        this.Amount = amount;

        sprite.sprite = item.icon;
    }
}
