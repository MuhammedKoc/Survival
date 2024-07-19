using System;
using System.Collections;
using System.Collections.Generic;
using Notification;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
    
public class Notify : PooledObject
{
    [SerializeField]
    protected NotifyUI ui;
    
    private NotifyData notifyData;
    public NotifyData NotifyData => notifyData;

    public void Init(NotifyData notifyData, Action onComplete)
    {
        this.notifyData = notifyData;
        
        ui.Init(notifyData, onComplete);
    }
}
