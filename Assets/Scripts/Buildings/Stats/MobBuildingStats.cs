using System;
using Mechanics;

namespace Buildings.Stats
{
    [Serializable]
    public class MobBuildingStats
    {
        public int _maxHp;
        public int _maxArmor;
        public ArmorType _armorType;
        public float _mobSpawnInterval;
    }
}