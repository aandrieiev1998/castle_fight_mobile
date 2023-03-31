using System;
using Match;
using Mechanics;
using Mobs;

namespace Scripts2.Mobs
{
    [Serializable]
    public class MageStats
    {
        public int _level;
        public float _hp;
        public float _armor;
        public ArmorType _armorType = ArmorType.LightArmor;
        public float _movementSpeed;
        public float _attackSpeed;
        public float _attackDamage;
        public float _visionRadius;
        public float _attackRadius;
        public AttackType _attackType = AttackType.Ranged;
        public TeamColor _teamColor;
    }
}