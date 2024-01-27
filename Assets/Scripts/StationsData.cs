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
        PlantPot = 3,
        Bookshelf = 4,
        FileCabinet = 5,
        Computer = 6,
        Photocopier = 7,
        Decoration = 8,
        Camera = 9,
        Vents = 10,
        VendingMachine =11,
        Oven =12,
        CoffeeMachine=13,
        ToiletPaper = 14,
        Mirror = 15,
        TrophyCabinet = 16
    }
}
