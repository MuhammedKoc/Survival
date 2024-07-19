using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Diagnostics;

public class InventoryNotifier : MonoBehaviour
{
    // [SerializeField] List<Notify> NotifyQueue = new List<Notify>();
    //
    // [SerializeField, Range(1, 10)] float NotifyDuration;
    //
    // [SerializeField] GameObject NotifyParentGB;
    //
    // [SerializeField] List<GameObject> NotifyGBList = new List<GameObject>();
    //
    // Dictionary<Notify, GameObject> NotifyToGameObject = new Dictionary<Notify, GameObject>();

    // private void Awake()
    // {
    //     for (int i = 0; i < NotifyParentGB.transform.childCount; i++)
    //     {
    //         NotifyGBList.Add(NotifyParentGB.transform.GetChild(i).gameObject);
    //     }
    // }
    //
    // public void NotifyItem(ItemObject item, int amount)
    // {
    //     ItemNotify itemNotify = new ItemNotify(item,amount);
    //
    //     for (int i = 0; i < NotifyQueue.Count; i++)
    //     {
    //         if (NotifyQueue[i] is ItemNotify notify)
    //         {  
    //             if(notify.item == item)
    //             {
    //                 notify.amount += amount;
    //
    //                 DOTween.Kill(NotifyToGameObject[notify].GetComponent<CanvasGroup>());
    //                 
    //                 UIElementActions(notify, NotifyToGameObject[notify]);
    //                 return;
    //             }
    //         }
    //     }
    //
    //     NotifyQueue.Add(itemNotify);
    //
    //     NotifyToGameObject.Add(itemNotify, NotifyGBList[0]);
    //
    //     UIElementActions(itemNotify, NotifyGBList[0]);
    //
    //     NotifyGBList.Insert(NotifyGBList.Count, NotifyGBList[0]);
    //
    //     NotifyGBList.RemoveAt(0);
    // }

    
    //
    // public void NotifyInventoryFull()
    // {
    //
    // }
    //
    // private void UIElementActions(Notify notify,GameObject gameObject)
    // {
    //     gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-715, -420 + (120 * NotifyQueue.IndexOf(notify)));
    //     gameObject.SetActive(true);
    //
    //     notify.UpdateUI(gameObject);
    //     
    //     IconAniamton(gameObject);
    //
    //     StopCoroutine(NotifyTransparentAnimation(notify));
    //
    //     StartCoroutine(NotifyTransparentAnimation(notify));
    // }
    //
    // IEnumerator NotifyTransparentAnimation(Notify notify)
    // {
    //     GameObject gameobject = NotifyToGameObject[notify];
    //
    //     gameobject.GetComponent<CanvasGroup>().alpha = 1f;
    //
    //     yield return new WaitForSeconds(NotifyDuration);
    //
    //     gameobject.GetComponent<CanvasGroup>().DOFade(0.2f, 1f);
    //
    //     yield return new WaitForSeconds(1f);
    //
    //     NotifyQueue.Remove(notify);
    //
    //     NotifyToGameObject.Remove(notify);
    //     
    //     gameobject.SetActive(false);
    //
    //     UpdateUINotifys();
    // }
    //
    // private void IconAniamton(GameObject gameobject)
    // {
    //     gameobject.transform.Find("Icon").DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f).SetEase(Ease.InOutBack).OnComplete (() => {
    //         gameobject.transform.Find("Icon").GetComponent<RectTransform>().DOScale(new Vector3(1f, 1f, 1f), 0.05f);
    //     });
    // }
    //
    // private void UpdateUINotifys()
    // {
    //     for (int i = 0; i < NotifyQueue.Count; i++)
    //     {
    //         GameObject NotifyGB = NotifyToGameObject[NotifyQueue[i]];
    //
    //         NotifyGB.GetComponent<RectTransform>().anchoredPosition = new Vector2(-715, -420 + (120*i));
    //
    //         NotifyGB.SetActive(true);
    //     }
    // }
}
