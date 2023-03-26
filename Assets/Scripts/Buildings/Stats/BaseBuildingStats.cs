using System;
using Mechanics;

namespace Buildings.Stats
{
    [Serializable]
    public class BaseBuildingStats
    {
        public int _maxHp;
        public int _maxArmor;
        public ArmorType _armorType;
    }
}