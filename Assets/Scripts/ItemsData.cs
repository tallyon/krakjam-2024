using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu]
public class ItemsData : ScriptableObject
{
    [SerializeField] List<ItemData> items;

    public CollectedItem GetCollectedItemPrefab(ItemsEnum itemsEnum)
    {
        return items.FirstOrDefault(x => x.itemEnum == itemsEnum).collectedItem;
    }

    public InteractableItem GetInteractableItemPrefab(ItemsEnum itemsEnum)
    {
        return items.FirstOrDefault(x => x.itemEnum == itemsEnum).interactableItem;
    }

    [Serializable]
    public class ItemData
    {
        public string Name;
        public ItemsEnum itemEnum;
        public CollectedItem collectedItem;
        public InteractableItem interactableItem;
    }

    public enum ItemsEnum
    {
        Cake = 0,
        Pork = 1
    }
}
