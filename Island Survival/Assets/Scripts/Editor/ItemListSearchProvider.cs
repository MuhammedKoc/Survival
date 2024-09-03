using System;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.Common;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor
{
    public class ItemListSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        private ItemObject[] listItems;
        private Action<ItemObject> onSetIndexCallBack;
        
        public ItemListSearchProvider(ItemObject[] items, Action<ItemObject> callBack)
        {
            listItems = items;
            onSetIndexCallBack = callBack;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchList = new List<SearchTreeEntry>();
            searchList.Add(new SearchTreeGroupEntry(new GUIContent("Items"), 0));
            
            string[] enumNames = Enum.GetNames(typeof(ItemType));
            for (int i = 0; i < enumNames.Length; i++)
            {
                searchList.Add(new SearchTreeGroupEntry(new GUIContent(enumNames[i]), 1));

                IEnumerable<ItemObject> items = listItems.Where(p => (int)p.type == i);

                foreach (var item in items)
                {
                    SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(item.name));
                    entry.userData = item;
                    entry.level = 2;
                    searchList.Add(entry);
                }
            }
            return searchList;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            onSetIndexCallBack?.Invoke((ItemObject)SearchTreeEntry.userData);
            return true;
        }
    }
}
