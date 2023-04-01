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
        [SerializeField] private float _damage;
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

        public float Damage
        {
            get => _damage;
            set => _damage = value;
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

        public float MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = value;
        }

        public TeamColor TeamColor { get; set; }
    }
}