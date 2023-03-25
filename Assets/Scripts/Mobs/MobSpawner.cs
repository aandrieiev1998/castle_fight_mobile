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

        private void BuildingAdded(MobBuildingDefinition mobBuildingDefinition, Vector3 position)
        {
            var coroutine = StartCoroutine(StartSpawningMobsForSingleBuilding(mobBuildingDefinition._spawnedMob, mobBuildingDefinition._stats._mobSpawnInterval,
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
                aiPath.radius = mobDefinition._pathfindingParameters._aiRadius;
                aiPath.height = mobDefinition._pathfindingParameters._aiHeight;
                aiPath.maxSpeed = mobDefinition._pathfindingParameters._aiMaxSpeed;

                var destinationSetter = mob.AddComponent<AIDestinationSetter>();
                destinationSetter.target = _mobsDefaultTarget;

                var mobTrigger = mob.AddComponent<CapsuleCollider>();
                mobTrigger.radius = mobDefinition._stats._visionRadius;
                mobTrigger.height = 100;
                mobTrigger.isTrigger = true;

                var mobAI = mob.AddComponent<MobAI>();

                spawnedMobs.Add(mob);
                Debug.Log($"Spawned mob: {mobDefinition._type}");
            }
        }
    }
}