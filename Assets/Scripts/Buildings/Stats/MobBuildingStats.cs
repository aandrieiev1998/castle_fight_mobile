using System;
using Mechanics;

namespace Buildings.Stats
{
    [Serializable]
    public class MobBuildingStats
    {
        public int maxHp;
        public int maxArmor;
        public ArmorType armorType;
        public float mobSpawnInterval;
    }
}