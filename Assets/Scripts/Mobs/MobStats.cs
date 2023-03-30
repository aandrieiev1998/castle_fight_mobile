using Mechanics;
using Stats;
using UnityEngine;

namespace Mobs
{
    [CreateAssetMenu(menuName = "CFM/MobStats")]
    public class MobStats : BaseStats
    {
        public MobType _mobType;
        public ArmorType _armorType;
        public AttackType _attackType;
    }
}