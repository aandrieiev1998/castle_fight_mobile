using Scripts3.Mechanics;

namespace Scripts3.Systems
{
    public interface IDamageSystem
    {
        public float Damage { get; set; }
        public DamageType DamageType { get; set; }
        public float AttackSpeed { get; set; }
        public float AttackDistance { get; set; }
    }
}