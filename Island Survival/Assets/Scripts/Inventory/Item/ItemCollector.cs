using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
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
    List<GameObject> TakenItems = new List<GameObject>();

    InventoryNotifier Notifier;

    private void Start()
    {
        Notifier = GetComponent<InventoryNotifier>();
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
            if (InventoryManager.Instance.CheckSpaceForItem(_item.item, _item.Amount))
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
                if (InventoryManager.Instance.AddItem(_item.item, _item.Amount, out int remainAmount))
                {
                    TakenItems.Add(Items[i].gameObject);
                    Notifier.NotifyItem(_item.item, _item.Amount);  
                    Debug.Log("Destroy"+ _item.Amount);
                    Destroy(Items[i].gameObject);
                }
                else
                {
                    Notifier.NotifyItem(_item.item, _item.Amount-remainAmount);
                    _item.Amount = remainAmount;
                    Debug.Log("else"+ _item.Amount);
                }
            }
        }
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
