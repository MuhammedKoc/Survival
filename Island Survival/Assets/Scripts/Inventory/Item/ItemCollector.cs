using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using MyBox;
using UnityEditor;

public class ItemCollector : MonoBehaviour
{
    [Separator("Collector Values")]
    [SerializeField]
    private float collectorSize;

    [SerializeField]
    private Vector2 collectorOffset;

    [Space(10)]
    [SerializeField]
    private float magnetSize;

    [SerializeField]
    private Vector2 magnetOffset;
    
    [Space(10)]
    [SerializeField]
    private LayerMask itemLayerMask;

    //TODO: notification managera geçince gerek kalmıyacak Instance ile halledilecek
    [SerializeField]
    private InventoryNotifier Notifier;

    //Privates
    private Collider2D[] detectedItems;
    private Collider2D[] items;
    
    //State'a taşınancak
    private void Update()
    {
        ItemMagnetic();
        ItemCollect();
    }

    private void ItemMagnetic()
    {
        detectedItems = Physics2D.OverlapCircleAll(magnetOffset + (Vector2)transform.position, magnetSize, itemLayerMask);
        foreach (var detectedItem in detectedItems)
        {
            Item _item = detectedItem.GetComponent<Item>();

            if (!_item.isMagnetable) return;
            if (!InventoryManager.Instance.CheckSpaceForItem(_item.item, _item.Amount)) return;

            detectedItem.transform.position =
                Vector2.MoveTowards(detectedItem.transform.position, this.transform.position, 0.03f);
        }
    }

    private void ItemCollect()
    {
        items = Physics2D.OverlapCircleAll(collectorOffset + (Vector2)transform.position, collectorSize, itemLayerMask);
        foreach (var item in items)
        {
            Item _item = item.gameObject.GetComponent<Item>();
            if (!_item.isMagnetable) return;

            if (InventoryManager.Instance.AddItem(_item.item, _item.Amount, out int remainAmount))
            {
                Notifier.NotifyItem(_item.item, _item.Amount);
                _item.ReturnToPool();
                _item.isMagnetable = true;
            }
            else
            {
                Notifier.NotifyItem(_item.item, _item.Amount - remainAmount);
                _item.Amount = remainAmount;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(magnetOffset + (Vector2)transform.position, magnetSize);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(collectorOffset + (Vector2)transform.position, collectorSize);
    }
}