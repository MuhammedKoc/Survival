using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemObject item;
    [Tooltip("Non-stackable items cannot have more than 1 amount")]
    public int Amount;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Deneme");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
