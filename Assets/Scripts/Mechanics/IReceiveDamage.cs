namespace Mechanics
{
    public interface IReceiveDamage
    {
        const float BuildArmorFactor = 1.2f;

        const float LightArmorFactor = 0.3f;

        const float MiddleArmorFactor = 0.5f;

        const float HeavyArmorFactor = 0.7f;

        const float MechanicArmorFactor = 0.5f;

        float FinalDamage(int health, int damage, int armor, ArmorType armorType)
        {
            float damageModifier = armorType switch
            {
                ArmorType.LightArmor => health - (damage / (armor * LightArmorFactor)),
                ArmorType.MidleArmor => health - (damage / (armor * MiddleArmorFactor)),
                ArmorType.HeavyArmor => health - (damage / (armor * HeavyArmorFactor)),
                ArmorType.MechanicArmor => health - (damage / (armor * MechanicArmorFactor)),
                ArmorType.Building => health - (damage / (armor * BuildArmorFactor))
            };
            return damageModifier;
        }
    }
}