using System;
using Match;
using Mechanics;
using Systems;
using UnityEngine;

namespace Mobs
{
    public abstract class Mob : MonoBehaviour, IHealthSystem, IDamageSystem, IBountySystem, ITeamSystem, IMovementSystem
    {
        [SerializeField] private float _rageDistance;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _healthAmount;
        [SerializeField] private float _healthRegen;
        [SerializeField] private float _armor;
        [SerializeField] private ArmorType _armorType;
        [SerializeField] private float _damageAmount;
        [SerializeField] private DamageType _damageType;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackDistance;
        [SerializeField] private int _goldForKill;
        [SerializeField] private int _experienceForKill;
        [SerializeField] private float _movementSpeed;

        public float RageDistance
        {
            get => _rageDistance;
            set => _rageDistance = value;
        }

        public float SpawnInterval
        {
            get => _spawnInterval;
            set => _spawnInterval = value;
        }

        public int GoldForKill
        {
            get => _goldForKill;
            set => _goldForKill = value;
        }

        public int ExperienceForKill
        {
            get => _experienceForKill;
            set => _experienceForKill = value;
        }

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

        public void InflictDamage(IHealthSystem healthSystem)
        {
            healthSystem.ReceiveDamage(DamageType, DamageAmount);
        }

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

        public float Armor
        {
            get => _armor;
            set => _armor = value;
        }

        public ArmorType ArmorType
        {
            get => _armorType;
            set => _armorType = value;
        }

        public TeamColor TeamColor { get; set; }

        public void ReceiveDamage(DamageType damageType, float damageAmount)
        {
            var damagePercentage = DamageUtils.GetDamagePercentage(ArmorType, damageType);
            var damageReduced = damageAmount * damagePercentage *
                                (1.0f - 0.06f * Armor / (1.0f + 0.06f * Math.Abs(Armor)));
            HealthAmount -= damageReduced;

            if (HealthAmount <= 0f)
            {
                Debug.Log($"{gameObject.name} has died");
                Destroy(gameObject);
            }
        }

        public float MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = value;
        }
    }
}