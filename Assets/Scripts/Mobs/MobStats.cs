using Mechanics;
using Scripts2.Mobs;
using UnityEngine;
using BaseStats = Stats.BaseStats;

namespace Mobs
{
    [CreateAssetMenu(menuName = "CFM/MobStats")]
    public class MobStats : BaseStats
    {
        public MobType _mobType;
        public ArmorType _armorType;
        public AttackType _attackType;
        public SwordsmanStats _swordsmanStats;
    }
}