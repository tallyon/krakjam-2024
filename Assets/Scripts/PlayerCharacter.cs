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

public class Ability
{
    public enum State
    {
        Ready,
        OnCooldown
    };

    public float Type { get; set; }
    public float CooldownEndTimeSeconds { get; set; }

    public void GoOnCooldown()
    {
        CooldownEndTimeSeconds = Time.deltaTime + 10;
    }
}
