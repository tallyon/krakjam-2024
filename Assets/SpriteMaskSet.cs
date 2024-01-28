using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteMask))]
public class SpriteMaskSet : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<SpriteMask>().sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
