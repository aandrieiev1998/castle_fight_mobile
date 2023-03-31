using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using Match;
using Mechanics;
using Pathfinding;
using Stats;
using UnityEngine;

namespace Mobs
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _mobPrefabs;
        [SerializeField] private BuildingContainer _buildingContainer;
        [SerializeField] private Transform _mobsParent; // todo refactor this for 2 teams
        [SerializeField] private List<TeamMaterial> _teamMaterials;

        private readonly Dictionary<MobType, GameObject> mobPrefabsDictionary = new();
        private readonly Dictionary<TeamColor, List<GameObject>> mobs = new();

        private void Start()
        {
            _buildingContainer.NewBuilding += BuildingAdded;
            _mobPrefabs.ForEach(mobPrefab =>
            {
                try
                {
                    mobPrefabsDictionary.Add(mobPrefab.GetComponent<MobBehaviour>()._mobStats._mobType, mobPrefab);
                }
                catch (Exception)
                {
                    Debug.LogError("Check mob scriptable objects. There is a duplicate MobType");
                }
            });

        }

        private void BuildingAdded(BuildingBehaviour buildingBehaviour)
        {
            if (buildingBehaviour._buildingData.activeStats.ContainsKey(StatType.MobSpawnRate))
            {
                StartCoroutine(StartSpawningMobsForSingleBuilding(buildingBehaviour._buildingData._teamColor,
                    buildingBehaviour._buildingStats._spawnedMob,
                    buildingBehaviour._buildingData.activeStats[StatType.MobSpawnRate]._currentValue,
                    buildingBehaviour.transform.position));
            }
        }

        private IEnumerator StartSpawningMobsForSingleBuilding(TeamColor teamColor, MobType mobType, float interval,
            Vector3 origin)
        {
            while (true)
            {
                //
                yield return new WaitForSeconds(interval);

                var mob = SpawnMob(teamColor, mobType, origin);

                if (!mobs.ContainsKey(teamColor))
                {
                    var mobsList = new List<GameObject> {mob};
                    mobs.Add(teamColor, mobsList);
                }
                else
                {
                    mobs[teamColor].Add(mob);
                }
            }
        }

        private GameObject SpawnMob(TeamColor mobTeamColor, MobType mobType, Vector3 position)
        {
            var mobPrefab = mobPrefabsDictionary[mobType];
            var mob = Instantiate(mobPrefab, position, Quaternion.identity, _mobsParent);

            var renderers = mob.GetComponentsInChildren<Renderer>();
            var teamMaterial = _teamMaterials.Single(tm => tm._teamColor == mobTeamColor);
            foreach (var rend in renderers)
            {
                rend.material = teamMaterial._material;
            }

            var mobBehaviour = mob.GetComponent<MobBehaviour>();
            mobBehaviour._mobData._teamColor = mobTeamColor;
            mobBehaviour._mobData._mobType = mobBehaviour._mobStats._mobType;
            mobBehaviour._mobData._armorType = mobBehaviour._mobStats._armorType;
            
            var mobHealth = mob.AddComponent<HealthSystem>();
            mobHealth._mobData = mobBehaviour._mobData;

            var aiPath = mob.AddComponent<AIPath>();
            aiPath.radius = mobBehaviour._pathfindingParameters._aiRadius;
            aiPath.height = mobBehaviour._pathfindingParameters._aiHeight;
            aiPath.maxSpeed = mobBehaviour._pathfindingParameters._aiMaxSpeed;

            var destinationSetter = mob.AddComponent<AIDestinationSetter>();

            var enemyThrone = _buildingContainer._buildings.Single(bb =>
                bb._buildingData._teamColor != mobTeamColor && bb._buildingData._buildingType == BuildingType.Throne);

            destinationSetter.target = enemyThrone.transform;

            var mobTrigger = mob.AddComponent<CapsuleCollider>();
            mobTrigger.radius = mobBehaviour._mobStats.Stats[StatType.VisionRadius];
            mobTrigger.height = 100;
            mobTrigger.isTrigger = true;

            var mobAI = mob.AddComponent<MobAI>();
            mobAI._mobDestinationSetter = destinationSetter;
            mobAI._buildingContainer = _buildingContainer;

            // Debug.Log($"Spawned mob: {mobDefinition._type}");
            return mob;
        }
    }
}