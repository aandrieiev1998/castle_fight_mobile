using System;
using Buildings.Stats;
using UnityEngine;

namespace Buildings.Definition
{
    [Serializable]
    public class BaseBuildingDefinition
    {
        public BuildingType _type;
        // public BaseBuildingStats _stats;
        public GameObject _prefab;
    }
}