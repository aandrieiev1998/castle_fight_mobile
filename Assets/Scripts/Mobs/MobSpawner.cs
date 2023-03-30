using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using Buildings.Data;
using Buildings.Definition;
using Buildings.Types;
using Match;
using Mechanics;
using Pathfinding;
using Scripts2.Mobs;
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
        private readonly Dictionary<PlayerTeam, List<GameObject>> mobs = new();

        private void Start()
        {
            _buildingContainer.NewMobBuilding += MobBuildingAdded;
            _mobPrefabs.ForEach(mobPrefab =>
            {
                try
                {
                    mobPrefabsDictionary.Add(mobPrefab.GetComponent<MobBehaviour>()._mobStats._mobType, mobPrefab);
                }
                catch (Exception e)
                {
                    Debug.LogError("Check mob scriptable objects. There is a duplicate MobType");
                }
            });

        }

        private void MobBuildingAdded(MobBuildingDefinition mobBuildingDefinition, MobBuildingData buildingData)
        {
            StartCoroutine(StartSpawningMobsForSingleBuilding(buildingData._playerTeam,
                mobBuildingDefinition._spawnedMob, mobBuildingDefinition._stats.mobSpawnInterval,
                buildingData.transform.position));
        }

        private IEnumerator StartSpawningMobsForSingleBuilding(PlayerTeam team, MobType mobType, float interval,
            Vector3 origin)
        {
            while (true)
            {
                //
                yield return new WaitForSeconds(interval);

                var mob = SpawnMob(team, mobType, origin);

                if (!mobs.ContainsKey(team))
                {
                    var mobsList = new List<GameObject> {mob};
                    mobs.Add(team, mobsList);
                }
                else
                {
                    mobs[team].Add(mob);
                }
            }
        }

        private GameObject SpawnMob(PlayerTeam mobTeam, MobType mobType, Vector3 position)
        {
            var mobPrefab = mobPrefabsDictionary[mobType];
            var mob = Instantiate(mobPrefab, position, Quaternion.identity, _mobsParent);

            var renderers = mob.GetComponentsInChildren<Renderer>();
            var teamMaterial = _teamMaterials.Single(tm => tm._playerTeam == mobTeam);
            foreach (var rend in renderers)
            {
                rend.material = teamMaterial._material;
            }

            // var mobData = mob.AddComponent<MobData>();
            // mobData._currentHp = mobDefinition._stats._maxHp;
            // mobData._currentDamage = mobDefinition._stats._damage;
            // mobData._currentArmor = mobDefinition._stats._armor;
            // mobData._currentArmorType = mobDefinition._stats._ArmorType;
            // mobData._currentTeam = mobTeam;

            // var mobHealth = mob.AddComponent<HealthSystem>();
            // mobHealth.Data = mobData;
            
            var aiPath = mob.AddComponent<AIPath>();
            var mobBehaviour = mobPrefab.GetComponent<MobBehaviour>();
            aiPath.radius = mobBehaviour._pathfindingParameters._aiRadius;
            aiPath.height = mobBehaviour._pathfindingParameters._aiHeight;
            aiPath.maxSpeed = mobBehaviour._pathfindingParameters._aiMaxSpeed;

            var destinationSetter = mob.AddComponent<AIDestinationSetter>();

            var enemyThrone = _buildingContainer._baseBuildings.Single(bd =>
                bd._playerTeam != mobTeam && bd._buildingType == BaseBuildingType.Throne);

            destinationSetter.target = enemyThrone.transform;

            var mobTrigger = mob.AddComponent<CapsuleCollider>();
            // mobTrigger.radius = mobDefinition._stats._visionRadius;
            mobTrigger.height = 100;
            mobTrigger.isTrigger = true;

            var mobAI = mob.AddComponent<MobAI>();
            mobAI.MobDestinationSetter = destinationSetter;

            // Debug.Log($"Spawned mob: {mobDefinition._type}");
            return mob;
        }
    }
}