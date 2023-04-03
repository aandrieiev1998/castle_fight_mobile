using System;
using System.Collections;
using System.Linq;
using Match;
using Mobs;
using Systems;
using UnityEngine;

namespace Buildings
{
    public abstract class MobBuilding : Building, IMobSpawningSystem
    {
        [SerializeField] private Mob _spawnedMob;

        public Mob SpawnedMob => _spawnedMob;

        public event Action<Mob> MobSpawned;

        public void SpawnMobs()
        {
            StartCoroutine(MobSpawningEnumerator());
        }

        private IEnumerator MobSpawningEnumerator()
        {
            yield return new WaitForSeconds(1.0f);

            while (true)
            {
                var spawnedMob = SpawnMob();
                MobSpawned?.Invoke(spawnedMob);

                yield return new WaitForSeconds(_spawnedMob.SpawnInterval);
            }
        }

        private Mob SpawnMob()
        {
            var mob = Instantiate(_spawnedMob, transform.position, Quaternion.identity);
            mob.TeamColor = TeamColor;

            var teamMaterialsContainer = FindObjectOfType<TeamMaterialsContainer>();
            var teamMaterial = teamMaterialsContainer.MobMaterials[TeamColor];
            var renderers = mob.GetComponentsInChildren<Renderer>();
            foreach (var rend in renderers) rend.material = teamMaterial;

            var enemyCastle = FindObjectsOfType<Castle>().Single(castle => castle.TeamColor != mob.TeamColor);

            var mobAI = mob.GetComponent<MobAI>();
            mobAI.TargetTransform = enemyCastle.transform;

            // Debug.Log($"Spawned mob: {mob.GetType()}");
            return mob;
        }
    }
}