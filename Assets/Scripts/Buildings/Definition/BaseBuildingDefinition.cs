using System;
using Buildings.Stats;
using Buildings.Types;
using UnityEngine;

namespace Buildings.Definition
{
    [Serializable]
    public class BaseBuildingDefinition
    {
        public BaseBuildingType _type;
        public GameObject _prefab;
        public BaseBuildingStats _stats;
    }
}