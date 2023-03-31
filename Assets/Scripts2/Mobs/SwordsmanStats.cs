using System;
using Match;
using Mechanics;
using Mobs;

namespace Scripts2.Mobs
{
    [Serializable]
    public class SwordsmanStats
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
        public AttackType _attackType = AttackType.Melee;
        public TeamColor _teamColor;
    }
}