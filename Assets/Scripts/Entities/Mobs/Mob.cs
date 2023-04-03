using Match;
using Systems.Impl;
using UnityEngine;

namespace Entities.Mobs
{
    public abstract class Mob : GameEntity
    {
        [SerializeField] private DamageSystem _damageSystem;
        [SerializeField] private MovementSystem _movementSystem;

        [SerializeField] private float _rageDistance;
        [SerializeField] private float _spawnInterval;


        public MovementSystem MovementSystem => _movementSystem;

        public DamageSystem DamageSystem => _damageSystem;

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

        private void Start()
        {
            var teamMaterialsContainer = FindObjectOfType<TeamMaterialsContainer>();
            var teamMaterial = teamMaterialsContainer.MobMaterials[TeamSystem.TeamColor];
            var renderers = GetComponentsInChildren<Renderer>();
            foreach (var rend in renderers) rend.material = teamMaterial;
        }
    }
}