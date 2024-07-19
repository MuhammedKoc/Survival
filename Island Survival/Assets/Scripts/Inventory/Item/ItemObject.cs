using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory/Item")]
public class ItemObject : ScriptableObject
{
    public LocalizedString localizedName;
    public LocalizedString localizedDescription;
    public ItemType type;
    public Sprite icon;

    [TextArea(15, 20)]
    private string Description;
    

    public bool stackable;
    [ConditionalField(nameof(stackable))]
    public int stacksize;

    private string oldName;

    private void Awake()
    {
        type = ItemType.Default;
    }

    [ButtonMethod]
    public void UpdateLocalizedStrings()
    {
        var namesTable = LocalizationEditorSettings.GetStringTableCollection("Items");
        var descTable = LocalizationEditorSettings.GetStringTableCollection("Items_Desc");

        if (namesTable.SharedData.Contains(oldName))
        {
            if (this.name == oldName) return;

            namesTable.SharedData.RemoveKey(oldName);
            descTable.SharedData.RemoveKey(oldName + "_desc");
        }

        namesTable.SharedData.AddKey(this.name);
        localizedName = new LocalizedString
            { TableReference = namesTable.TableCollectionName, TableEntryReference = this.name };

        var nameEnTable = namesTable.GetTable("en") as StringTable;
        nameEnTable.AddEntry(this.name, this.name);


        descTable.SharedData.AddKey(this.name + "_desc");
        localizedDescription = new LocalizedString
            { TableReference = descTable.TableCollectionName, TableEntryReference = this.name + "_desc" };

        var descEnTable = descTable.GetTable("en") as StringTable;
        descEnTable.AddEntry(this.name + "_desc", Description);

        oldName = this.name;

        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(namesTable);
        EditorUtility.SetDirty(namesTable.SharedData);
    }

    public string GetLocalizedType()
    {
        var typesable = LocalizationEditorSettings.GetStringTableCollection("ItemTypes");
        var typeString = Enum.GetName(typeof(ItemType),type);
        
        if (typesable.SharedData.Contains(typeString))
        {
            LocalizedString localizedString = new LocalizedString { TableReference = typesable.TableCollectionName, TableEntryReference = typeString };

            return localizedString.GetLocalizedString();
        }

        return null;
    }
}

public enum ItemType
{
    Weapon,
    Food,
    Default
}
