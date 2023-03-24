using System;
using Mechanics;

namespace Buildings
{
    [Serializable]
    public struct BuildingStats
    {
        public int _maxHp;
        public int _armor;
        public ArmorType armorType;
        public float _mobSpawnInterval;
    }
}