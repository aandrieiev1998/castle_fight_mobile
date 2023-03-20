using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using UnityEngine;

namespace Mobs
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] private List<MobEntry> _mobPrefabs;
        [SerializeField] private BuildingContainer _buildingContainer;

        // private Dictionary<Coroutine, GameObject> buildings;
        private List<GameObject> spawnedMobs = new();

        private void Start()
        {
            _buildingContainer.newActiveBuilding += BuildingAdded;
        }

        private void BuildingAdded(BuildingEntry buildingEntry, Vector3 position)
        {
            StartCoroutine(SpawnMob(buildingEntry._spawnedMob, buildingEntry._stats._mobSpawnInterval,
                position));
        }


        private IEnumerator SpawnMob(MobType mobType, float interval, Vector3 origin)
        {
            while (true)
            {
                //
                yield return new WaitForSeconds(interval);
                
                var mobToSpawn = _mobPrefabs.Single(entry => entry._type == mobType)._prefab;
                var spawnedMob = Instantiate(mobToSpawn, origin, Quaternion.identity);
                spawnedMobs.Add(spawnedMob);
                Debug.Log("Mob has been spawned");
            }
        }
    }
}