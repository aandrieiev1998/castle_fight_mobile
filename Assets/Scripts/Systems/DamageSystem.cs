using System;
using Mechanics;
using UnityEngine;

namespace Systems
{
    [Serializable]
    public class DamageSystem
    {
        [SerializeField] private float _damageAmount;
        [SerializeField] private DamageType _damageType;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackDistance;

        public float DamageAmount
        {
            get => _damageAmount;
            set => _damageAmount = value;
        }

        public DamageType DamageType
        {
            get => _damageType;
            set => _damageType = value;
        }

        public float AttackSpeed
        {
            get => _attackSpeed;
            set => _attackSpeed = value;
        }

        public float AttackDistance
        {
            get => _attackDistance;
            set => _attackDistance = value;
        }

        public void InflictDamage(HealthSystem healthSystem)
        {
            healthSystem.ReceiveDamage(DamageType, DamageAmount);
        }
    }
}