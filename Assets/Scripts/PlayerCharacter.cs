using System;
using UnityEngine;
using static ItemsData;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterTypeEnum characterTypeEnum;
    public CollectedItem collectedItem = null;
    public Action<ItemsEnum> onItemAdd;
    public Action onItemDeleted;

    public void AddItem(ItemsEnum item)
    {
        var collected = GameStateController.Instance.GetCollectedItemPrefab(item);
        collectedItem = collected;
        onItemAdd?.Invoke(item);
    }

    public void DeleteItem()
    {
        collectedItem = null;
        onItemDeleted?.Invoke();
    }
}
