using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
    
public class Notify
{
    public string Message;
    public Sprite icon;



    public Notify(string message, Sprite icon)
    {
        Message = message;
        this.icon = icon;
    }

    public virtual void UpdateUI(GameObject gameobject)
    {
        gameobject.transform.Find("Icon").GetComponent<Image>().sprite = icon;
        gameobject.transform.Find("Message").GetComponent<TextMeshProUGUI>().text = Message;
    }
}

public class ItemNotify : Notify
{
    public ItemObject item;
    public int amount;

    public ItemNotify(ItemObject item, int amount) : base(null, null)
    {
        this.item = item;
        this.amount = amount;
    }

    public override void UpdateUI(GameObject gameObject)
    {
        gameObject.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;

        gameObject.transform.Find("Message").GetComponent<TextMeshProUGUI>().text = item.name;

        if (amount > 1)
        {
            gameObject.transform.Find("ItemAmount").GetComponent<TextMeshProUGUI>().text = amount.ToString();
        }
        else
        {
            gameObject.transform.Find("ItemAmount").GetComponent<TextMeshProUGUI>().text = null;
        }
    }
}
