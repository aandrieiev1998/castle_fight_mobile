using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using Pathfinding;
using UnityEngine;

namespace Mobs
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] private List<MobDefinition> _mobPrefabs;
        [SerializeField] private BuildingContainer _buildingContainer;
        [SerializeField] private Transform _mobsParent;
        [SerializeField] private Transform _mobsDefaultTarget;

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

                var mobDefinition = _mobPrefabs.Single(entry => entry._type == mobType);
                var mobPrefab = mobDefinition._prefab;
                var mob = Instantiate(mobPrefab, origin, Quaternion.identity, _mobsParent);

                var mobData = mob.AddComponent<MobData>();
                mobData._currentHp = mobDefinition._stats._maxHp;

                var aiPath = mob.AddComponent<AIPath>();
                aiPath.radius = mobDefinition._aiRadius;
                aiPath.height = mobDefinition._aiHeight;
                aiPath.maxSpeed = mobDefinition._aiMaxSpeed;

                var destinationSetter = mob.AddComponent<AIDestinationSetter>();
                destinationSetter.target = _mobsDefaultTarget;

                spawnedMobs.Add(mob);
                Debug.Log($"Spawned mob: {mobDefinition._type}");
            }
        }
    }
}