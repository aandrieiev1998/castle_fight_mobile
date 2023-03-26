using System;
using Mechanics;

namespace Buildings.Stats
{
    [Serializable]
    public class BaseBuildingStats
    {
        public int maxHp;
        public int damage;
        public int maxArmor;
        public ArmorType armorType;
    }
}