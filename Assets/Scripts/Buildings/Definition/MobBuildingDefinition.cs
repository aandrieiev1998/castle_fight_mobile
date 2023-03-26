﻿using System;
using Buildings.Stats;
using Buildings.Types;
using Mobs;
using UnityEngine;

namespace Buildings.Definition
{
    [Serializable]
    public class MobBuildingDefinition
    {
        public MobBuildingType _type;
        public GameObject _prefab;
        public MobType _spawnedMob;
        public MobBuildingStats _stats;
    }
}