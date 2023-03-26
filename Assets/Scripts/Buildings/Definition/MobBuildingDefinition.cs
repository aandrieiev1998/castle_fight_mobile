using System;
using Buildings.Stats;
using Mobs;
using UnityEngine;

namespace Buildings.Definition
{
    [Serializable]
    public class MobBuildingDefinition
    {
        public BuildingType _type;
        public GameObject _prefab;
        public MobBuildingStats _stats;
        public MobType _spawnedMob;
    }
}