using System;
using UnityEngine;

namespace Buildings
{
    [Serializable]
    public class BaseBuildingDefinition
    {
        public BuildingType _type;
        public BaseBuildingStats _stats;
        public GameObject _prefab;
    }
}