using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu]
public class ItemsData : ScriptableObject
{
    public List<ItemData> items { get; private set; }
    public List<SpecialGroupItems> itemsGroup { get; private set; }

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
        public int minPointsValue;
        public int maxPointsValue;
        public int finalPointsValue;
        public bool canBeGivenToGirl;
    }

    [Serializable]
    public class SpecialGroupItems
    {
        public ItemsGroup itemEnum;
        public List<ItemsEnum> items;
    }

    public enum ItemsGroup
    {
        ColdBeverages = 0,
        HotBeverages = 1,
        Food = 2,
        Poems = 3
    }

    public enum ItemsEnum
    {
        Cake = 0,
        Pork = 1,
        Cappuchino =2,
        Cola = 3,
        PoemOfButts = 4,
    }
}
