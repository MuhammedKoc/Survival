using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemDropper : MonoBehaviour
{
    [SerializeField] GameObject Player;

    public void ItemDrop(ItemObject item, int Amount)
    {
        //Her item in ayrı prefabı olmasına gerek yok. Item prefab olur Init ile değiştirilir
        
        GameObject ItemGb = Instantiate(item.Prefab);
        ItemGb.transform.position = Player.transform.position;

        ItemGb.GetComponent<Item>().Amount = Amount;

        StartCoroutine(IgnoreOverlap(ItemGb));
        Vector2 pos = new Vector2(Player.transform.position.x + (PlayerController.Instance.Move.LastDirection.x*1.5f), Player.transform.position.y+(PlayerController.Instance.Move.LastDirection.y*1.5f));
        ItemGb.transform.DOMove(pos, 0.5f);
    }
    
    IEnumerator IgnoreOverlap(GameObject item)
    {
        item.layer = 0;
        yield return new WaitForSeconds(0.5f);
        item.layer = LayerMask.NameToLayer("Item");
    }
}
