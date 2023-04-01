using Match;
using Mobs;
using Scripts3.Buildings;
using Systems;
using UnityEngine;

namespace Buildings
{
    public abstract class MobBuilding : Building, IMobSpawningSystem
    {

        [SerializeField] private Mob _spawnedMob;

        public Mob SpawnedMob => _spawnedMob;

        public abstract void SpawnMobs();
        
        internal Mob SpawnMob()
        {
            var mob = Instantiate(_spawnedMob, transform.position, Quaternion.identity);
        
            var renderers = mob.GetComponentsInChildren<Renderer>();
            var teamMaterial = TeamMaterials.MobMaterials[TeamColor];
            foreach (var rend in renderers)
            {
                rend.material = teamMaterial;
            }
            
            mob.TeamColor = TeamColor;
        
            // var mobHealth = mob.AddComponent<HealthSystem>();
            // mobHealth._mobData = mob._mobData;
            //
            // var aiPath = mob.AddComponent<AIPath>();
            // aiPath.radius = mob._pathfindingParameters._aiRadius;
            // aiPath.height = mob._pathfindingParameters._aiHeight;
            // aiPath.maxSpeed = mob._pathfindingParameters._aiMaxSpeed;
            //
            // var destinationSetter = mob.AddComponent<AIDestinationSetter>();
            //
            // var enemyThrone = _buildingContainer._buildings.Single(bb =>
            //     bb._buildingData._teamColor != mobTeamColor && bb._buildingData._buildingType == BuildingType.Throne);
            //
            // destinationSetter.target = enemyThrone.transform;
            //
            // var mobTrigger = mob.AddComponent<CapsuleCollider>();
            // mobTrigger.radius = mob._mobStats.Stats[StatType.VisionRadius];
            // mobTrigger.height = 100;
            // mobTrigger.isTrigger = true;
            //
            // var mobAI = mob.AddComponent<MobAI>();
            // mobAI._mobDestinationSetter = destinationSetter;
            // mobAI._buildingContainer = _buildingContainer;
        
            // Debug.Log($"Spawned mob: {mobDefinition._type}");
            return mob;
        }
        
        
    }
}