using System;
using Mobs;
using UnityEngine;

namespace Buildings
{
    [Serializable]
    public class MobBuildingDefinition : BaseBuildingDefinition
    {
        public MobType _spawnedMob;
        public new MobBuildingStats _stats;
    }
}