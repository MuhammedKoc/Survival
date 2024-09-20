using System;
using System.Collections.Generic;
using Editor;
using Tmn.Debugs;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DebugHelperWindow : EditorWindow
{
    private SerializedObject serializedObject;
    private static EditorWindow window;
    private GUIStyle foldoutStyle;
    
    private bool showSurvivalStatus;
    private bool showInventory;
    private bool showLog;
    
    //Survival Status
    private float health;
    private float hunger;
    private float thirst;
    
    //Invenyory
    private ItemObject item;
    private int amount = 1;

    private List<ItemObject> items = new List<ItemObject>();
    public List<string> keys = new List<string>() {"Data/Items"};
    
    //Logger
    private DebugHelper helper => DebugHelper.Instance;
    
    public bool isPlayer;
    public bool isSystem;
    public bool isUI;

    
    private void OnEnable()
    {
        items.Clear();
        
        AsyncOperationHandle<IList<ItemObject>> itemsAsync = Addressables.LoadAssetsAsync<ItemObject>("Items", item =>
        {
            items.Add(item);
        });
    }

    
    [MenuItem("Window/Debug Helper")]
    public static void OpenWindow() 
    {
        window = EditorWindow.GetWindow(typeof(DebugHelperWindow));
        window.titleContent.text = "Debug Helper";
        window.titleContent.image = Resources.Load<Texture>("Sprites/DebugHelperIcon");
        window.minSize = new Vector2(300, 300);
    }
    
    private void OnGUI()
    {
         foldoutStyle = new GUIStyle(EditorStyles.foldout);
        foldoutStyle.fontStyle = FontStyle.Bold;
        foldoutStyle.fontSize = 14;
        
        SurvivalStatusFoldout();
        EditorGUILayout.Space(10);
        InventoryFoldout();
        EditorGUILayout.Space(10);
        LogsFoldout();
    }

    #region Survival Status

    private void SurvivalStatusFoldout()
    {
        showSurvivalStatus = EditorGUILayout.Foldout(showSurvivalStatus, "Survival Status", foldoutStyle);

        EditorGUI.indentLevel++;
         
        if (Application.isPlaying && showSurvivalStatus)
        {
            PlayerHealth playerHealth = PlayerHealth.Instance;
            SurvivalStatus survivalStatus = SurvivalStatus.Instance;
        
            health = SurvivalStatusLine("Health", (i) => playerHealth.Increase(i), (i) => playerHealth.Decrease(i),
                health, playerHealth.CurrentHealth);

            hunger = SurvivalStatusLine("Hunger", (i) => survivalStatus.IncreaseHunger(i), (i) => survivalStatus.DecreaseHunger(i),
                hunger, survivalStatus.CurrentHunger);
        
            thirst = SurvivalStatusLine("Thirst", (i) => survivalStatus.IncreaseThirst(i), (i) => survivalStatus.DecreaseThirst(i),
                thirst, survivalStatus.CurrentThirst);
        }
        else if (!Application.isPlaying && showSurvivalStatus)
        {
            EditorGUILayout.HelpBox("Survival Status will be displayed when play mode", MessageType.Info);
        }
        
        EditorGUI.indentLevel--;
    }

    private int SurvivalStatusLine(string Label,Action<int> increase, Action<int> deccrease, float value ,float currentValue)
    {
        int intField = 0;
        
        EditorGUILayout.BeginHorizontal();

        EditorGUIUtility.labelWidth = 80;
        intField = EditorGUILayout.IntField(new GUIContent(Label), (int)value, GUILayout.Width(200));
        
        if (GUILayout.Button("+", GUILayout.Width(25)))
        {
            increase.Invoke(intField);
        }
        if (GUILayout.Button("-", GUILayout.Width(25)))
        {
            deccrease.Invoke(intField);
        }
        
        if (GUILayout.Button("=", GUILayout.Width(25)))
        {
            if (intField > currentValue)
            {
                increase.Invoke((int)(intField-currentValue));
            }
            else if(intField < currentValue)
            {
                deccrease.Invoke((int)(currentValue-intField));
            }
        }
        
        EditorGUILayout.EndHorizontal();

        return intField;
    }

    #endregion

    #region Inventory

    private void InventoryFoldout()
    {
        showInventory = EditorGUILayout.Foldout(showInventory, "Inventory", foldoutStyle);
        EditorGUI.indentLevel++;
        
        if (Application.isPlaying && showInventory)
        {
            EditorGUILayout.BeginHorizontal();
            
            EditorGUIUtility.labelWidth = 110;
            item = EditorGUILayout.ObjectField(new GUIContent("Selected Item"), item,typeof(ItemObject), GUILayout.Width(250)) as ItemObject;
            
            if (GUILayout.Button(new GUIContent(Resources.Load<Texture>("Sprites/SearchIcon")), EditorStyles.iconButton, GUILayout.Width(EditorGUIUtility.singleLineHeight), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                ItemsSearchWindow();
            }
            
            if (GUILayout.Button("Add", GUILayout.Width(45)))
            {
                if(amount < 0) return;
                InventoryManager.Instance.AddItem(item, amount, out int a);
            }
            
            if (GUILayout.Button("Clear Slots", GUILayout.Width(85)))
            {
                InventoryManager.Instance.ClearSlots();
            }
            
            EditorGUILayout.EndHorizontal();

            amount = EditorGUILayout.IntField("Amount", amount, GUILayout.Width(250));
        }
        else if (!Application.isPlaying && showInventory)
        {
            EditorGUILayout.HelpBox("Inventory will be displayed when play mode", MessageType.Info);
        }
        
        EditorGUI.indentLevel--;
    }

    private void ItemsSearchWindow()
    {
        SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)),
            new ItemListSearchProvider(items.ToArray(), x => { item = x;}));
    }

    #endregion

    private void LogsFoldout()
    {
        showLog = EditorGUILayout.Foldout(showLog, "Logs", foldoutStyle);
        EditorGUI.indentLevel++;

        if(!showLog) return;
        
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
            
        isPlayer = EditorGUILayout.Toggle("Is Player", isPlayer);
        isSystem = EditorGUILayout.Toggle("Is System", isSystem);
        isUI = EditorGUILayout.Toggle("Is UI", isUI);
            
        EditorGUILayout.EndVertical();
        
        if (Application.isPlaying && helper != null)
        {
            EditorGUILayout.BeginVertical();

            if (GUILayout.Button("Recover Player Logs"))
            {
                helper.RecoverLogs(TmmLogType.Player);
            }
            
            if (GUILayout.Button("Recover System Logs"))
            {
                helper.RecoverLogs(TmmLogType.System);
            }
            
            if (GUILayout.Button("Recover UI Logs"))
            {
                helper.RecoverLogs(TmmLogType.UI);
            }
            
            if (helper.IsPlayer != isPlayer)
                helper.IsPlayer = isPlayer;
            
            if (helper.IsSystem != isSystem)
                helper.IsSystem = isSystem;
            
            if (helper.IsUI != isUI)
                helper.IsUI = isUI;
            
            EditorGUILayout.EndVertical();
        }
        
        EditorGUILayout.EndHorizontal();
    }
}
