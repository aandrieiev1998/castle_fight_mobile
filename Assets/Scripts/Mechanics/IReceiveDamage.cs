namespace Mechanics
{
    public interface IReceiveDamage
    {
        const float BuildArmorFactor = 1.2f;

        const float LightArmorFactor = 0.3f;

        const float MiddleArmorFactor = 0.5f;

        const float HeavyArmorFactor = 0.7f;

        const float MechanicArmorFactor = 0.5f;

        public float FinalDamage(int damage, float armor, ArmorType armorType);
    }
}