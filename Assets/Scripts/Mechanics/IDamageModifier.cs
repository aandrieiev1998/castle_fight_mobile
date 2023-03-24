using Buildings;

namespace Mechanics
{

    public interface IDamageModifier
    {
        const double buildArmorFactor = 1.2;

        const double lightArmorFactor = 0.3;

        const double midleArmorFactor = 0.5;

        const double heavyArmorFactor = 0.7;

        const double mechanicArmorFactor = 0.5;

        double DamageModifier(double damage, ArmorType armorType)
        {
            double finalDamage = armorType switch
            {
                ArmorType.LightArmor => lightArmorFactor * damage,
                ArmorType.MidleArmor => midleArmorFactor * damage,
                ArmorType.HeavyArmor => heavyArmorFactor * damage,
                ArmorType.MechanicArmor => mechanicArmorFactor * damage,
                ArmorType.Building => buildArmorFactor * damage
            };
            return finalDamage;
        }
    }
}