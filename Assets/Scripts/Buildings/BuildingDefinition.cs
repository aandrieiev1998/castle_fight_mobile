using System;
using Mobs;
using UnityEngine;

namespace Buildings
{
    [Serializable]
    public struct BuildingDefinition
    {
        public BuildingType _type;
        public BuildingStats _stats;
        public MobType _spawnedMob;
        public GameObject _prefab;
    }
}