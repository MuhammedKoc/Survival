using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory/Item")]
public class ItemObject : ScriptableObject
{
    //TODO: Gereksiz publicleri, SerializeField'lere çevir
    
    public string ItemName;
    public ItemType type;
    public Sprite icon;

    [SerializeField]

    [TextArea(15, 20)]
    public string Description;

    public bool stackable;
    
    [ConditionalField(nameof(stackable))]
    public int stacksize;

    private void Awake()
    {
        type = ItemType.Default;
    }
}

public enum ItemType
{
    Weapon,
    Magical,
    Potion,
    Food,
    Default
}
