using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCollector : MonoBehaviour
{
    [Header("Collector Values")]
    [SerializeField] private float CollectorSize;
    [SerializeField] private Vector2 CollectorOffset;
    [Space(10)]
    [SerializeField] LayerMask Mask;

    [SerializeField] Collider2D[] DetectedItems;

    InventoryManager inventoryManager;

    bool OnceRun = false;

    private void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
    }

    private void Update()
    {
        ItemDetector();
    }

    void ItemDetector()
    {
        
        DetectedItems = Physics2D.OverlapCircleAll(CollectorOffset + (Vector2)transform.position, CollectorSize,Mask);
        //inventory de yer varsa kendine çekicek

        for (int i = 0; i < DetectedItems.Length; i++)
        {
            DetectedItems[i].gameObject.GetComponent<Transform>().DOMove(this.transform.position, 1.5f);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(CollectorOffset + (Vector2)transform.position, CollectorSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Item"))
        {
            ItemObject item = collision.gameObject.GetComponent<Item>().item;
            if (inventoryManager.Inventory.AddItem(item, 1))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
