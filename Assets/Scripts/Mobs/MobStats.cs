using System;
using Mechanics;

namespace Mobs
{
    [Serializable]
    public class MobStats
    {
        public int _maxHp;
        public int _damage;
        public int _armor;
        public ArmorType ArmorType;
        public float _visionRadius;
    }
}