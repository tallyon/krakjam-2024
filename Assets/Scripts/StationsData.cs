using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu]
public class StationsData : ScriptableObject
{
    [SerializeField] List<StationData> stations;

    public InteractableStations GetInteractableStationPrefab(StationEnum stationEnum)
    {
        return stations.FirstOrDefault(x => x.stationEnum == stationEnum).interactableItem;
    }

    [Serializable]
    public class StationData
    {
        public string Name;
        public StationEnum stationEnum;
        public InteractableStations interactableItem;
    }

    public enum StationEnum
    {
        Fridge = 0,
        Board = 1,
        Door =2,
    }
}
