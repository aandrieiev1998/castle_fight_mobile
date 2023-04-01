using System.Collections.Generic;
using Scripts3.Mechanics;

namespace Scripts3.Systems
{
    public static class DamageUtils
    {
        static Dictionary<DamageType, Dictionary<ArmorType, float>> damageMatrix = new()
        {
            {
                DamageType.NormalDamage, new Dictionary<ArmorType, float>
                {
                    { ArmorType.LightArmor, 1.5f },
                    { ArmorType.MediumArmor, 1.0f },
                    { ArmorType.HeavyArmor, 0.7f },
                    { ArmorType.MechanicArmor, 0.6f },
                    { ArmorType.HeroArmor, 0.5f },
                    { ArmorType.BuildingArmor, 0.5f }
                }
            },
            {
                DamageType.SiegeDamage, new Dictionary<ArmorType, float>
                {
                    { ArmorType.LightArmor, 0.9f },
                    { ArmorType.MediumArmor, 0.8f },
                    { ArmorType.HeavyArmor, 0.6f },
                    { ArmorType.MechanicArmor, 1.0f },
                    { ArmorType.HeroArmor, 0.5f },
                    { ArmorType.BuildingArmor, 2.0f }
                }
            },
            {
                DamageType.MagicDamage, new Dictionary<ArmorType, float>
                {
                    { ArmorType.LightArmor, 0.9f },
                    { ArmorType.MediumArmor, 1.0f },
                    { ArmorType.HeavyArmor, 1.5f },
                    { ArmorType.MechanicArmor, 0.5f },
                    { ArmorType.HeroArmor, 0.5f },
                    { ArmorType.BuildingArmor, 0.3f }
                }
            },
            {
                DamageType.HeroDamage, new Dictionary<ArmorType, float>
                {
                    { ArmorType.LightArmor, 1.0f },
                    { ArmorType.MediumArmor, 1.0f },
                    { ArmorType.HeavyArmor, 1.0f },
                    { ArmorType.MechanicArmor, 1.0f },
                    { ArmorType.HeroArmor, 1.0f },
                    { ArmorType.BuildingArmor, 1.0f }
                }
            },
        };

        public static float GetDamagePercentage(ArmorType armorType, DamageType damageType)
        {
            return damageMatrix[damageType][armorType];
        }
    }
}