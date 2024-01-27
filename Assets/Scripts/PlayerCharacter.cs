using UnityEngine;
using static ItemsData;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterTypeEnum characterTypeEnum;
    public CollectedItem collectedItem = null;

    public void AddItem(ItemsEnum item)
    {
        var collected = GameStateController.Instance.GetCollectedItemPrefab(item);
        collectedItem = collected;
    }

    public void DeleteItem()
    {
        collectedItem = null;
    }
}
