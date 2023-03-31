using System;
using Match;
using Mechanics;

namespace Scripts2.Buildings
{
    [Serializable]
    public class ThroneStats
    {
        public float _hp;
        public float _armor;
        public ArmorType _armorType = ArmorType.Building;
        public TeamColor _teamColor;
    }
}