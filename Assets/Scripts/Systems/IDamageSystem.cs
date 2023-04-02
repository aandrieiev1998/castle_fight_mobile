using Mechanics;

namespace Systems
{
    public interface IDamageSystem
    {
        public float DamageAmount { get; set; }
        public DamageType DamageType { get; set; }
        public float AttackSpeed { get; set; }
        public float AttackDistance { get; set; }

        public void InflictDamage(IHealthSystem healthSystem);
    }
}