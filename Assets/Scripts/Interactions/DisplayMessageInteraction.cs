using System;
using UnityEngine;

public class DisplayMessageInteraction : Interaction
{
    public string Message;

    public override bool PlayInteraction(string playerTag)
    {
        if (playerTag == "Player1")
        {
            OnInteraction?.Invoke(this);
        }

        return false;
    }
}
