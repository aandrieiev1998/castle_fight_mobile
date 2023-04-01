using System.Collections;
using System.Linq;
using Match;
using Mobs;
using Pathfinding;
using Systems;
using UnityEngine;

namespace Buildings
{
    public abstract class MobBuilding : Building, IMobSpawningSystem
    {

        [SerializeField] private Mob _spawnedMob;

        public Mob SpawnedMob => _spawnedMob;

        public void SpawnMobs()
        {
            StartCoroutine(MobSpawningEnumerator());
        }

        private IEnumerator MobSpawningEnumerator()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnedMob.SpawnInterval);

                SpawnMob();
            }
        }

        private Mob SpawnMob()
        {
            var mob = Instantiate(_spawnedMob, transform.position, Quaternion.identity);
            mob.TeamColor = TeamColor;

            var teamMaterialsContainer = FindObjectOfType<TeamMaterialsContainer>();
            var teamMaterial = teamMaterialsContainer.MobMaterials[TeamColor];
            var renderers = mob.GetComponentsInChildren<Renderer>();
            foreach (var rend in renderers)
            {
                rend.material = teamMaterial;
            }

            var aiPath = mob.GetComponent<AIPath>();
            aiPath.maxSpeed = mob.MovementSpeed;

            var castlesInScene = FindObjectsOfType<Castle>();
            var enemyCastle = castlesInScene.Single(castle => castle.TeamColor != TeamColor);
            
            var destinationSetter = mob.GetComponent<AIDestinationSetter>();
            destinationSetter.target = enemyCastle.transform;
            
            Debug.Log($"Spawned mob: {mob.GetType()}");
            return mob;
        }
        
        
    }
}