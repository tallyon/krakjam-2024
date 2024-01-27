using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterTypeEnum characterTypeEnum;
    public CollectedItem collectedItem = null;

    public void AddItem(ItemsEnum item)
    {

        //collectedItem = item;
    }

    public void DeleteItem()
    {
        collectedItem = null;
    }
}
