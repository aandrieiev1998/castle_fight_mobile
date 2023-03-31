using System;
using Mechanics;

namespace Scripts2.Buildings
{
    [Serializable]
    public class TowerStats
    {
        public float _hp;
        public float _armor;
        public ArmorType _armorType = ArmorType.Building;
        public float _visionRadius;
        public float _attackSpeed;
        public float _attackDamage;
    }
}