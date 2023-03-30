﻿using Mobs;
using Stats;
using UnityEngine;

namespace Mechanics
{
    public class HealthSystem : MonoBehaviour, IReceiveDamage
    {
        public MobData _mobData;
        
        public void TakeDamage(int damage)
        {
            _mobData.activeStats[StatType.Health]._currentValue -= FinalDamage(damage, _mobData.activeStats[StatType.Armor]._currentValue, _mobData._armorType);
            if (_mobData.activeStats[StatType.Health]._currentValue <= 0)
            {
                Die();
            }
            Debug.Log($"Received damage {damage} current health = {_mobData.activeStats[StatType.Health]._currentValue}");
        }
        private void Die()
        {
            Destroy(gameObject);
        }

        public float FinalDamage(int damage, float armor, ArmorType armorType)
        {
            if (armor <= 0)
            {
                armor = 0.01f;
            }
            float damageModifier = armorType switch
            {
                ArmorType.LightArmor => damage / (armor * IReceiveDamage.LightArmorFactor),
                ArmorType.MidleArmor => damage / (armor * IReceiveDamage.MiddleArmorFactor),
                ArmorType.HeavyArmor => damage / (armor * IReceiveDamage.HeavyArmorFactor),
                ArmorType.MechanicArmor => damage / (armor * IReceiveDamage.MechanicArmorFactor),
                ArmorType.Building => damage / (armor * IReceiveDamage.BuildArmorFactor)
            };
            return damageModifier;
        }
    }
}