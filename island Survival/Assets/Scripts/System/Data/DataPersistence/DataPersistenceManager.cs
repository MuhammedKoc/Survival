using System.Collections.Generic;
using System.Linq;
using Inventory.InventoryBack;
using MyBox;
using Tmn.Debugs;
using UnityEngine;

namespace Tmn.Data
{
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
                    DebugHelper.LogSystem(instance.GetType().Name + "Instance is Null");
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

        [SerializeField]
        private bool useEncryption;


        #region Privates

        private GameData data;
        private FileDataHandler dataHandler;

        #endregion

        private List<IDataPersistence> dataPersistences = new List<IDataPersistence>();

        private void Start()
        {
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
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
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

            InventoryManager invenManager = FindObjectOfType<InventoryManager>();

            data.InventorySlots.Clear();
            for (int i = 0; i < invenManager.InventorySlotCount + invenManager.SlotbarSlotCount; i++)
            {
                data.InventorySlots.Add(new Slot(i, null, 0));
            }

            dataHandler.Save(data);
        }

        [ButtonMethod]
        public void DecryptSave()
        {
            if (!useEncryption) return;

            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
            data = dataHandler.Load();

            useEncryption = false;
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

            dataHandler.Save(data);
        }

        [ButtonMethod]
        public void EncryptSave()
        {
            if (useEncryption) return;

            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
            data = dataHandler.Load();

            useEncryption = true;
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

            dataHandler.Save(data);
        }

        private List<IDataPersistence> FindAllIDataPersistences()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects =
                FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }

    }
}
