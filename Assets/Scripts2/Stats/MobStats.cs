using Mobs;
using Scripts2.Stats.Enums;
using UnityEngine;

namespace Scripts2.Stats
{
    [CreateAssetMenu(menuName = "CFM/MobStats")]
    public class MobStats : BaseStats
    {
        public MobType _mobType;
        public AttackType _attackType;
    }
}