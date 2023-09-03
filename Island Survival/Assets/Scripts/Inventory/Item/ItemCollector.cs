using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEditor;

public class ItemCollector : MonoBehaviour
{
    [Header("Collector Values")]
    [SerializeField] private float CollectorSize;
    [SerializeField] private Vector2 CollectorOffset;
    [Space(10)]
    [SerializeField] private float MagnetSize;
    [SerializeField] private Vector2 MagnetOffset;
    [SerializeField] private LayerMask Mask;

    [SerializeField] Collider2D[] DetectedItems;
    [SerializeField] Collider2D[] Items;
    InventoryManager inventoryManager;
    List<GameObject> TakenItems = new List<GameObject>();

    private void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
    }

    private void Update()
    {    
        ItemMagnetic();
        ItemCollect();
    }

    void ItemMagnetic()
    {
        DetectedItems = Physics2D.OverlapCircleAll(MagnetOffset + (Vector2)transform.position, MagnetSize, Mask);
        for (int i = 0; i < DetectedItems.Length; i++)
        {
            Item _item = DetectedItems[i].GetComponent<Item>();
            if (inventoryManager.Inventory.CheckSpaceForItem(_item.item, _item.Amount))
            {
                DetectedItems[i].transform.position = Vector2.MoveTowards(DetectedItems[i].transform.position, this.transform.position, 0.03f);
            }
        }

    }

    void ItemCollect()
    {
        Items = Physics2D.OverlapCircleAll(CollectorOffset + (Vector2)transform.position, CollectorSize, Mask);
        for (int i = 0; i < Items.Length; i++)
        {
            if (!TakenItems.Contains(Items[i].gameObject))
            {
                Item _item = Items[i].gameObject.GetComponent<Item>();
                if (inventoryManager.Inventory.AddItem(_item.item, _item.Amount))
                {
                    TakenItems.Add(Items[i].gameObject);
                    Destroy(Items[i].gameObject);
                }
            }
        }
    }

    void print()
    {
        Debug.Log("as");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(MagnetOffset + (Vector2)transform.position, MagnetSize);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(CollectorOffset + (Vector2)transform.position, CollectorSize);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Mask == (Mask | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Item");
            ItemObject item = collision.gameObject.GetComponent<Item>().item;
            if (inventoryManager.Inventory.AddItem(item, 1))
            {
                TakenItems.Remove(collision.gameObject);
                Destroy(collision.gameObject);
            }
        }
    }*/
}
