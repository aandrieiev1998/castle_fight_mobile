using System;
using Match;
using Mechanics;
using Mobs;
using Pathfinding;
using Scripts3.Mobs;
using Scripts3.Systems;
using Stats;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts3.Buildings
{
    public abstract class MobBuilding : Building, IMobSpawningSystem
    {

        [SerializeField] private Mob _spawnedMob;

        public Mob SpawnedMob => _spawnedMob;

        public abstract void SpawnMobs();
        
        // internal GameObject SpawnMob()
        // {
        //     var mob = Instantiate(_spawnedMob, transform.position, Quaternion.identity);
        //
        //     var renderers = mob.GetComponentsInChildren<Renderer>();
        //     var teamMaterial = _teamMaterials.Single(tm => tm._teamColor == mobTeamColor);
        //     foreach (var rend in renderers)
        //     {
        //         rend.material = teamMaterial._material;
        //     }
        //
        //     var mobBehaviour = mob.GetComponent<MobBehaviour>();
        //     mobBehaviour._mobData._teamColor = mobTeamColor;
        //     mobBehaviour._mobData._mobType = mobBehaviour._mobStats._mobType;
        //     mobBehaviour._mobData._armorType = mobBehaviour._mobStats._armorType;
        //
        //     var mobHealth = mob.AddComponent<HealthSystem>();
        //     mobHealth._mobData = mobBehaviour._mobData;
        //
        //     var aiPath = mob.AddComponent<AIPath>();
        //     aiPath.radius = mobBehaviour._pathfindingParameters._aiRadius;
        //     aiPath.height = mobBehaviour._pathfindingParameters._aiHeight;
        //     aiPath.maxSpeed = mobBehaviour._pathfindingParameters._aiMaxSpeed;
        //
        //     var destinationSetter = mob.AddComponent<AIDestinationSetter>();
        //
        //     var enemyThrone = _buildingContainer._buildings.Single(bb =>
        //         bb._buildingData._teamColor != mobTeamColor && bb._buildingData._buildingType == BuildingType.Throne);
        //
        //     destinationSetter.target = enemyThrone.transform;
        //
        //     var mobTrigger = mob.AddComponent<CapsuleCollider>();
        //     mobTrigger.radius = mobBehaviour._mobStats.Stats[StatType.VisionRadius];
        //     mobTrigger.height = 100;
        //     mobTrigger.isTrigger = true;
        //
        //     var mobAI = mob.AddComponent<MobAI>();
        //     mobAI._mobDestinationSetter = destinationSetter;
        //     mobAI._buildingContainer = _buildingContainer;
        //
        //     // Debug.Log($"Spawned mob: {mobDefinition._type}");
        //     return mob;
        // }
        
        
    }
}