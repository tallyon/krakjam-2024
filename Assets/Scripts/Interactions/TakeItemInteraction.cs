using System;
using UnityEngine;

public class TakeItemInteraction : Interaction
{

    public override bool PlayInteraction(string playerTag)
    {
        if(playerTag == "Player1")
        {
            OnInteraction?.Invoke(new DisplayMessageInteraction() { Message = "Item cannot be taken right now" });
            return false;
        }
        else
        {
            OnInteraction?.Invoke(this);
            return true;
        }
    }
}
