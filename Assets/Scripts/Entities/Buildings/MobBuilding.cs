using System.Collections;
using Buildings;
using Systems;
using UnityEngine;

namespace Entities.Buildings
{
    public abstract class MobBuilding : Building
    {

        [SerializeField] private MobSpawningSystem _mobSpawningSystem;

        public override void Start()
        {
            base.Start();
            StartCoroutine(MobSpawningEnumerator());
        }

        private IEnumerator MobSpawningEnumerator()
        {
            yield return new WaitForSeconds(1.0f);

            for (var i = 0; i < 100; i++)
            {
                _mobSpawningSystem.SpawnMob(transform.position, TeamSystem.TeamColor);
                
                yield return new WaitForSeconds(_mobSpawningSystem.MobPrefab.SpawnInterval);
            }

            // while (true)
            // {
            //     _mobSpawningSystem.SpawnMob(transform.position, TeamSystem.TeamColor);
            //
            //     yield return new WaitForSeconds(_mobSpawningSystem.MobPrefab.SpawnInterval);
            // }
        }

        
    }
}