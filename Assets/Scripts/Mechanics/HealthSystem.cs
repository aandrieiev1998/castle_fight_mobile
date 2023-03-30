using Mobs;
using UnityEngine;

namespace Mechanics
{
    public class HealthSystem : MonoBehaviour, IReceiveDamage
    {
        public MobData Data;
        private float currentHealth;
        private int currentArmor;
        private ArmorType currentArmorType;
        
        private void Start()
        {
            currentHealth = Data._currentHp;
            currentArmor = Data._currentArmor;
            currentArmorType = Data._currentArmorType;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= FinalDamage(damage, this.currentArmor, currentArmorType);
            if (currentHealth <= 0)
            {
                Die();
            }
            Debug.Log($"Received damage {damage} current health = {currentHealth}");
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