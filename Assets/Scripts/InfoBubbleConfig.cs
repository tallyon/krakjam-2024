using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static SimpleTextPopAnimation;

[CreateAssetMenu]
public class InfoBubbleConfig : ScriptableObject
{
    public List<InfoData> infoPrefabs;

    public GameObject GetCollectedItemPrefab(InfoEnums infoEnums)
    {
        return infoPrefabs.FirstOrDefault(x => x.infoEnums == infoEnums).prefab;
    }

    [Serializable]
    public class InfoData
    {
        public InfoEnums infoEnums;
        public GameObject prefab;
    }
}
