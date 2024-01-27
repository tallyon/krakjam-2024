using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static ItemsData;

public class GirlInteractable : Interactable
{
    [SerializeField] ItemsData itemConfig;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteSad;
    [SerializeField] Sprite spriteNormal;
    [SerializeField] Sprite spriteHappy;
    [SerializeField] int showEmotiontime = 1500;

    private bool _wasColdBevarageDelivered;
    private bool _wasHotBeverageDelivered;
    private bool _wasFoodDelivered;
    private bool _wasPoemDelivered;

    private List<ItemData> itemsWithPoints = new();

    private void Awake()
    {
        _wasColdBevarageDelivered = false;
        _wasHotBeverageDelivered = false;
        _wasFoodDelivered = false;
        _wasPoemDelivered = false;
    }

    private void Start()
    {
        GetPointsForThisItemsRound();
    }

    public void GetPointsForThisItemsRound()
    {
        foreach(var item in itemConfig.items)
        {
            if (item.canBeGivenToGirl)
            {
                item.finalPointsValue = Random.Range(item.minPointsValue, item.maxPointsValue + 1);
                itemsWithPoints.Add(item);
            }
        }
    }

    protected override void HandleOnInteractionWithObject(Interaction interaction, CharacterTypeEnum characterTypeEnum)
    {
        base.HandleOnInteractionWithObject(interaction, characterTypeEnum);

        var player = GameStateController.Instance.GetPlayerObject(characterTypeEnum);

        switch (interaction)
        {
            case GirlGiveItemInteraction giveItemInteraction:
                var itemData = itemsWithPoints.FirstOrDefault(x => x.itemEnum == player.collectedItem.itemsEnum);
                if(itemData != null)
                {
                    if(itemConfig.IsItemInGroup(ItemsGroup.ColdBeverages, itemData.itemEnum))
                    {
                        if (!_wasColdBevarageDelivered)
                        {
                            _wasColdBevarageDelivered = true;
                        }
                        else
                        {
                            Debug.Log($"Girl: Girl does not any mnore {ItemsGroup.ColdBeverages} by item {itemData.itemEnum}");
                            ShowSad();
                            player.DeleteItem();
                            return;
                        }
                    }

                    if (itemConfig.IsItemInGroup(ItemsGroup.HotBeverages, itemData.itemEnum))
                    {
                        if (!_wasHotBeverageDelivered)
                        {
                            _wasHotBeverageDelivered = true;
                        }
                        else
                        {
                            Debug.Log($"Girl: Girl does not any mnore {ItemsGroup.HotBeverages} by item {itemData.itemEnum}");
                            ShowSad();
                            player.DeleteItem();
                            return;
                        }
                    }

                    if (itemConfig.IsItemInGroup(ItemsGroup.Food, itemData.itemEnum))
                    {
                        if (!_wasFoodDelivered)
                        {
                            _wasFoodDelivered = true;
                        }
                        else
                        {
                            Debug.Log($"Girl: Girl does not any mnore {ItemsGroup.Food} by item {itemData.itemEnum}");
                            ShowSad();
                            player.DeleteItem();
                            return;
                        }
                    }

                    if (itemConfig.IsItemInGroup(ItemsGroup.Poems, itemData.itemEnum))
                    {
                        if (!_wasPoemDelivered)
                        {
                            _wasPoemDelivered = true;
                        }
                        else
                        {
                            Debug.Log($"Girl: Girl does not any mnore {ItemsGroup.Poems} by item {itemData.itemEnum}");
                            ShowSad();
                            player.DeleteItem();
                            return;
                        }
                    }

                    player = GameStateController.Instance.GetPlayerObject(characterTypeEnum);
                    GivePoints(itemData.finalPointsValue, player);
                    player.DeleteItem();
                }
                else
                {
                    Debug.Log($"Girl: Girl does not want {itemData.itemEnum}");
                    ShowSad();
                }
                break;
        }
    }

    private void GivePoints(int points, PlayerCharacter player)
    {
        //var player = GameStateController.Instance.GetP
        Debug.Log($"Girl gives {player.characterTypeEnum}: {points}");
        if (points > 0)
        {
            ShowHappy();
        }
        else
        {
            ShowSad();
        }
    }

    private async void ShowHappy()
    {
        Debug.Log($"Girl: Girl is happy");
        spriteRenderer.sprite = spriteHappy;
        await Task.Delay(showEmotiontime);
        spriteRenderer.sprite = spriteNormal;
    }

    private async void ShowSad()
    {
        Debug.Log($"Girl: Girl is sad");
        spriteRenderer.sprite = spriteSad;
        await Task.Delay(showEmotiontime);
        spriteRenderer.sprite = spriteNormal;
    }
}
