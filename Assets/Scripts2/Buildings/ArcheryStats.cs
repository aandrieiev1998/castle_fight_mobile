using System;
using Match;
using Mechanics;
using Mobs;

namespace Scripts2.Buildings
{
    [Serializable]
    public class ArcheryStats
    {
        public float _hp;
        public float _armor;
        public ArmorType _armorType = ArmorType.Building;
        public TeamColor _teamColor;
        public float _mobSpawnInterval;
        public MobType _mobType = MobType.Archer;
    }
}