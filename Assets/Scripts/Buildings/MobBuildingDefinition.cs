using System;
using Mobs;
using UnityEngine;

namespace Buildings
{
    [Serializable]
    public class MobBuildingDefinition
    {
        public BuildingType _type;
        public MobBuildingStats _mobStats;
        public GameObject _prefab;
        
        public MobType _spawnedMob;
    }
}