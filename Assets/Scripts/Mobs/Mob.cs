using System;
using Systems.Impl;
using UnityEngine;

namespace Mobs
{
    public abstract class Mob : MonoBehaviour
    {
        [SerializeField] private DamageSystem _damageSystem;
        [SerializeField] private HealthSystem _healthSystem;
        [SerializeField] private MovementSystem _movementSystem;

        [SerializeField] private float _rageDistance;
        [SerializeField] private float _spawnInterval;
        

        public MovementSystem MovementSystem => _movementSystem;

        public DamageSystem DamageSystem => _damageSystem;

        public HealthSystem HealthSystem => _healthSystem;

        public TeamSystem TeamSystem { get; } = new();

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
    }
}