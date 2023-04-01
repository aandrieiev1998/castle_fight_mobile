using Mechanics;

namespace Systems
{
    public interface IHealthSystem
    {
        public float HealthAmount { get; set; }
        public float HealthRegen { get; set; }
        public float Armor { get; set; }
        public ArmorType ArmorType { get; set; }

        public void TakeDamage(DamageType damageType, float damageAmount)
        {
            float damagePercentage = DamageUtils.GetDamagePercentage(ArmorType, damageType);
            float damageReduced = (damageAmount * damagePercentage);
            HealthAmount -= damageReduced;
        }
    }
}