using System;
using Mechanics;
using UnityEngine;

namespace Systems
{
    [Serializable]
    public class HealthSystem
    {
        [SerializeField] private float _armorAmount;
        [SerializeField] private ArmorType _armorType;
        [SerializeField] private float _healthAmount;
        [SerializeField] private float _healthRegen;

        public float HealthAmount
        {
            get => _healthAmount;
            set => _healthAmount = value;
        }

        public float HealthRegen
        {
            get => _healthRegen;
            set => _healthRegen = value;
        }

        public float ArmorAmount
        {
            get => _armorAmount;
            set => _armorAmount = value;
        }

        public ArmorType ArmorType
        {
            get => _armorType;
            set => _armorType = value;
        }

        public void ReceiveDamage(DamageType damageType, float damageAmount)
        {
            if (IsDead) return;
            
            var damagePercentage = DamageUtils.GetDamagePercentage(ArmorType, damageType);
            var damageReduced = damageAmount * damagePercentage *
                                (1.0f - 0.06f * ArmorAmount / (1.0f + 0.06f * Math.Abs(ArmorAmount)));
            HealthAmount -= damageReduced;

            if (HealthAmount > 0f) return;
            
            IsDead = true;
            Death?.Invoke();
        }
        
        public bool IsDead { get; set; }

        public event Action Death;
    }
}