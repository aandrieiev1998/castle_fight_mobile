using System;
using System.Linq;
using Buildings;
using Entities.Buildings;
using Entities.Buildings.MobBuildings;
using Match;
using UI.Controllers;
using UnityEngine;
using Random = System.Random;

// using Random = UnityEngine.Random;

namespace Bots
{
    public class BotController : MonoBehaviour
    {
        [SerializeField] private TeamSelectionMenuController _teamSelectionMenuController;
        [SerializeField] private BuildingSpawner _buildingSpawner;

        private void Start()
        {
            _teamSelectionMenuController.PlayerTeamSelected += OnPlayerTeamSelected;
        }

        private void OnPlayerTeamSelected(TeamColor teamColor)
        {
            var buildingPlatforms = FindObjectsOfType<BuildingPlatform>().ToArray();

            // var botBuilding1Type = typeof(Archery);
            // var botBuilding1Team = buildingPlatforms[0].TeamColor;
            // _buildingSpawner.SpawnMobBuilding(botBuilding1Type, botBuilding1Team, buildingPlatforms[0].transform.position);
            // buildingPlatforms[0].IsOccupied = true;
            //
            // var botBuilding2Type = typeof(Barracks);
            // var botBuilding2Team = buildingPlatforms[1].TeamColor;
            // _buildingSpawner.SpawnMobBuilding(botBuilding2Type, botBuilding2Team, buildingPlatforms[1].transform.position);
            // buildingPlatforms[1].IsOccupied = true;
            
            var botTeam = teamColor switch
            {
                TeamColor.Blue => TeamColor.Red,
                TeamColor.Red => TeamColor.Blue,
                _ => throw new Exception($"Unsupported team color: {teamColor}")
            };
            
            foreach (var buildingPlatform in buildingPlatforms)
            {
                if (buildingPlatform.TeamColor != botTeam || buildingPlatform.IsOccupied) continue;
                
                var botBuildingType = new Random().NextDouble() > 0.5d ? typeof(Barracks) : typeof(Archery);
                // var botBuildingType = typeof(Archery);
                _buildingSpawner.SpawnMobBuilding(botBuildingType, botTeam, buildingPlatform.transform.position);
                buildingPlatform.IsOccupied = true;
            }

        }
    }
}