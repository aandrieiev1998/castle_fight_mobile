using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using UnityEngine;

namespace Mobs
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] private List<MobDefinition> _mobPrefabs;
        [SerializeField] private BuildingContainer _buildingContainer;

        private Dictionary<BuildingData, Coroutine> cePizda = new();
        private List<GameObject> spawnedMobs = new();

        private void Start()
        {
            _buildingContainer.NewActiveBuilding += BuildingAdded;
        }

        private void BuildingAdded(BuildingDefinition buildingDefinition, Vector3 position)
        {
            var coroutine = StartCoroutine(StartSpawningMobsForSingleBuilding(buildingDefinition._spawnedMob, buildingDefinition._stats._mobSpawnInterval,
                position));
        }

        private IEnumerator StartSpawningMobsForSingleBuilding(MobType mobType, float interval, Vector3 origin)
        {
            while (true)
            {
                //
                yield return new WaitForSeconds(interval);

                var mobToSpawnDefinition = _mobPrefabs.Single(entry => entry._type == mobType);
                var mobToSpawn = mobToSpawnDefinition._prefab;
                var spawnedMob = Instantiate(mobToSpawn, origin, Quaternion.identity);
                spawnedMob.AddComponent<MobData>();

                spawnedMobs.Add(spawnedMob);
                Debug.Log("Mob has been spawned");
            }
        }
    }
}