using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory/Item")]
public class ItemObject : ScriptableObject
{
    public string ItemName;
    public ItemType type;
    public Sprite icon;
    public GameObject Prefab;

    [SerializeField]

    [TextArea(15, 20)]
    public string Description;

    public bool stackable;
    public int stacksize
    {
        get { return 32; }
    }

    private void Awake()
    {
        type = ItemType.Default;
    }

    public virtual void Use()
    {
        Debug.Log("Item");
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
