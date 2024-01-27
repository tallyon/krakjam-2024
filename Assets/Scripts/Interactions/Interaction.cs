using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Action<Interaction> OnInteraction;
    public virtual bool PlayInteraction(string playerTag)
    {
        return true;
    }
}
