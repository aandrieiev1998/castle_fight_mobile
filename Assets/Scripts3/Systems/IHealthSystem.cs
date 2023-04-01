using Scripts3.Mechanics;

namespace Scripts3.Systems
{
    public interface IHealthSystem
    {
        public float HealthAmount { get; set; }
        public float HealthRegen { get; set; }
        public float Armor { get; set; }
        public ArmorType ArmorType { get; set; }
    }
}