using System;
using Mechanics;

namespace Buildings
{
    [Serializable]
    public struct BuildingStats
    {
        public int _maxHp;
        public int _armor;
        public float _mobSpawnInterval;
    }
}