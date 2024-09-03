using System;
using System.Collections;
using System.Collections.Generic;
using Tmn.Debug;
using UnityEngine;
using UnityEngine.AddressableAssets;
using LogType = UnityEngine.LogType;

public class DebugHelper : MonoBehaviour
{
    [HideInInspector]
    public bool IsPlayer = true;
    
    [HideInInspector]
    public bool IsSystem = true;
    
    [HideInInspector]
    public bool IsUI = true;

    #region Privates

    private static List<LogMessage> playerLogs = new List<LogMessage>();
    private static List<LogMessage> systemLogs = new List<LogMessage>();
    private static List<LogMessage> uiLogs = new List<LogMessage>();

    #endregion

    #region Singleton

    private static DebugHelper instance = null;

    public static DebugHelper Instance
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

    public static void LogPlayer(string msg)
    {
        Log(TmmLogType.Player, msg); 
    }
    
    public static void LogSystem(string msg)
    {
        Log(TmmLogType.System, msg); 
    }
    
    public static void LogUI(string msg)
    {
        Log(TmmLogType.UI, msg); 
    }

    private static void Log(TmmLogType type ,string msg)
    {
        bool showLog = false;
        
        switch (type)
        {
            case TmmLogType.Player:
                showLog = Instance.IsPlayer;
                playerLogs.Add(new LogMessage(GetTimeHMS(), msg));
                break;
            
            case TmmLogType.System:
                showLog = Instance.IsSystem;
                systemLogs.Add(new LogMessage(GetTimeHMS(), msg));
                break;
            
            case TmmLogType.UI:
                showLog = Instance.IsUI;
                uiLogs.Add(new LogMessage(GetTimeHMS(), msg));
                break;
        }
        
        if(showLog)
            Debug.Log("<color=#f5faff>" + msg + "</color>");
    }

    private static string GetTimeHMS()
    {
        return DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
    }

    public void RecoverLogs(TmmLogType type)
    {
        List<LogMessage> recoverLogs = new List<LogMessage>();
        
        switch (type)
        {
            case TmmLogType.Player:
                recoverLogs = playerLogs;
                break;
            
            case TmmLogType.System:
                recoverLogs = systemLogs;
                break;
            
            case TmmLogType.UI:
                recoverLogs = uiLogs;
                break;
        }

        foreach (var log in recoverLogs)
        {
            Debug.Log( "<color=#B8D8D7> Recover : </color>" + "<color=#f5faff>" + log.message + "</color>");
        }
    }
}