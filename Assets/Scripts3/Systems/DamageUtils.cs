using System;
using System.Collections.Generic;
using Mechanics;

namespace Scripts3.Systems
{
    public static class DamageUtils
    {
        static Dictionary<DamageType, Dictionary<ArmorType, float>> damageMatrix= new()
        {
            { DamageType.NormalDamage, new Dictionary<ArmorType, float> {
                { ArmorType.LightArmor, 1.0f },
                { ArmorType.MediumArmor, 1.5f },
                { ArmorType.HeavyArmor, 1.0f },
                { ArmorType.Building, 0.5f }
            }},
            { DamageType.SiegeDamage, new Dictionary<ArmorType, float> {
                // { ArmorType.A, 5.0f },
                // { ArmorType.B, 6.0f },
                // { ArmorType.C, 7.0f },
                // { ArmorType.D, 8.0f }
            }},
            // { DamageType.Third, new Dictionary<ArmorType, float> {
            //     // { ArmorType.A, 9.0f },
            //     // { ArmorType.B, 10.0f },
            //     // { ArmorType.C, 11.0f },
            //     // { ArmorType.D, 12.0f }
            // }}
        };

        public static float GetDamagePercentage(ArmorType armorType, DamageType damageType)
        {
            return damageMatrix[damageType][armorType];
        }
            
    }
}