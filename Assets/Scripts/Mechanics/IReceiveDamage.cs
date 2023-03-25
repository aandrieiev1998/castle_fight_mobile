namespace Mechanics
{
    public interface IReceiveDamage
    {
        const float buildArmorFactor = 1.2f;

        const float lightArmorFactor = 0.3f;

        const float midleArmorFactor = 0.5f;

        const float heavyArmorFactor = 0.7f;

        const float mechanicArmorFactor = 0.5f;

        float DamageModifier(int health, int damage, int armor, ArmorType armorType)
        {
            float finalDamage = armorType switch
            {
                ArmorType.LightArmor => health - (damage / (armor * lightArmorFactor)),
                ArmorType.MidleArmor => health - (damage / (armor * midleArmorFactor)),
                ArmorType.HeavyArmor => health - (damage / (armor * heavyArmorFactor)),
                ArmorType.MechanicArmor => health - (damage / (armor * mechanicArmorFactor)),
                ArmorType.Building => health - (damage / (armor * buildArmorFactor))
            };
            return finalDamage;
        }
    }
}