using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemDropper : MonoBehaviour
{
    [SerializeField]
    private Item itemPrefab;

    [SerializeField]
    private float dropDistance;

    [SerializeField]
    private float doTweenDuration;
    
    #region Singleton

    private static ItemDropper instance = null;

    public static ItemDropper Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log(instance.GetType().Name + "Instance is Null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    public void ItemDrop(ItemObject item, int amount)
    {
        Vector2 playerPos = PlayerController.Instance.transform.position;
        Vector2 lastDirection = PlayerController.Instance.Move.LastDirection;
        
        Item itemGO = (Item)ObjectPool.Instance.Get(itemPrefab);
        itemGO.Init(item, amount);
        itemGO.transform.position = playerPos;
        
        Vector2 destinationPos = new Vector2(playerPos.x + (lastDirection.x*dropDistance), playerPos.y+(lastDirection.y*dropDistance));

        itemGO.isMagnetable = false;
        itemGO.transform.DOMove(destinationPos, doTweenDuration).OnComplete(() =>
        {
            itemGO.isMagnetable = true;
        });
    }
}
