using System;
using Buildings.Stats;
using UnityEngine;

namespace Buildings.Definition
{
    [Serializable]
    public class BaseBuildingDefinition
    {
        public BuildingType _type;
        public GameObject _prefab;
        public BaseBuildingStats _stats;
    }
}