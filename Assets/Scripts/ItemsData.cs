using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu]
public class ItemsData : ScriptableObject
{
    public List<ItemData> items;
    public List<SpecialGroupItems> itemsGroup;

    public CollectedItem GetCollectedItemPrefab(ItemsEnum itemsEnum)
    {
        return items.FirstOrDefault(x => x.itemEnum == itemsEnum).collectedItem;
    }

    public InteractableItem GetInteractableItemPrefab(ItemsEnum itemsEnum)
    {
        return items.FirstOrDefault(x => x.itemEnum == itemsEnum).interactableItem;
    }

    public bool IsItemInGroup(ItemsGroup givenGroup, ItemsEnum itemsEnum)
    {
        var group = itemsGroup.FirstOrDefault(x => x.groupEnum == givenGroup);

        if (group == null) return false;

        return group.items.Contains(itemsEnum);
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
        public ItemsGroup groupEnum;
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
        WateringCan = 5,
        FireExtinguisher = 6,
        EnergyDrink = 7,
        Flower = 8,
        Soap = 9,
        Coin = 10,
        Photo = 11,
        Disk = 12,
        Confetti = 13,
        CopyOfTheButt = 14,
        Sandwich = 15,
        NotMadeCake = 16,
        RawPork = 17,
        Latte = 18,
        BlackCoffe = 19,
        BakedCake = 20,
        RoastedPork = 21,
        PoemAboutHorses = 22,
        PoemAboutDeaths = 23,
        PoemAboutButts = 24,
        Flowe = 25,
        Weights = 26,
        Juice = 27,
        Trophy = 28,
        Origami = 29,
        None = 30,
        ToiletPaper =31,
        Marker = 32
    }
}
