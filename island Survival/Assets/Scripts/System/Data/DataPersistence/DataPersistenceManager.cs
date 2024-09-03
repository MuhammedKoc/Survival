using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory.InventoryBack;
using MyBox;
using Tmn.Data;
using Tmn.Data.DataPersistence;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    #region Singleton

    private static DataPersistenceManager instance = null;

    public static DataPersistenceManager Instance
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
    
    [Header("File Storage Confif")]
    [SerializeField]
    private string fileName;


    #region Privates

    private GameData data;
    private FileDataHandler dataHandler;

    #endregion

    private List<IDataPersistence> dataPersistences = new List<IDataPersistence>();

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistences = FindAllIDataPersistences();
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistence in dataPersistences)
        {
            dataPersistence.SaveData(ref data);
        }   
     
        dataHandler.Save(data);
        
        DebugHelper.LogSystem("Game Saved");
    }

    public void LoadGame()
    {
        this.data = dataHandler.Load();
        
        if (this.data == null)
        {
            DebugHelper.LogSystem("No GameData was found.");
            NewGame();
        }
        
        foreach (IDataPersistence dataPersistence in dataPersistences)
        {
            dataPersistence.LoadData(data);
        }   
        
        DebugHelper.LogSystem("Game Loaded");
    }

    public void NewGame()
    {
        this.data = new GameData();
        
        InventoryManager invenManager = InventoryManager.Instance; 
        
        data.InventorySlots.Clear();
        for (int i = 0; i < invenManager.InventorySlotCount + invenManager.SlotbarSlotCount; i++)
        {
            data.InventorySlots.Add(new Slot(i, null, 0));
        }
        
        dataHandler.Save(data);
    }
    
    
    [ButtonMethod]
    public void NewGameForEdit()
    {
        this.data = new GameData();
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        InventoryManager invenManager = FindObjectOfType<InventoryManager>(); 
        
        data.InventorySlots.Clear();
        for (int i = 0; i < invenManager.InventorySlotCount + invenManager.SlotbarSlotCount; i++)
        {
            data.InventorySlots.Add(new Slot(i, null, 0));
        }
        
        dataHandler.Save(data);
    }

    private List<IDataPersistence> FindAllIDataPersistences()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    
}
