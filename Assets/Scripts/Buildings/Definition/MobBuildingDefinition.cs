using System;
using Buildings.Stats;
using Mobs;
using UnityEngine;

namespace Buildings.Definition
{
    [Serializable]
    public class MobBuildingDefinition : BaseBuildingDefinition
    {
        // public MobBuildingStats _mobStats;
        
        public MobType _spawnedMob;
    }
}