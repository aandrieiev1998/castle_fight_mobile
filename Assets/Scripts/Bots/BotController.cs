using System;
using System.Linq;
using Buildings;
using Buildings.MobBuildings;
using Entities.Buildings;
using Match;
using UI.Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

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
            var botTeam = teamColor switch
            {
                TeamColor.Blue => TeamColor.Red,
                TeamColor.Red => TeamColor.Blue,
                _ => throw new Exception($"Unsupported team color: {teamColor}")
            };

            var buildingPlatforms = FindObjectsOfType<BuildingPlatform>().Where(buildingPlatform =>
                buildingPlatform.TeamColor == botTeam && !buildingPlatform.IsOccupied).ToList();
            foreach (var buildingPlatform in buildingPlatforms)
            {
                var value = Random.value;
                // var botBuildingType = value > 0.5f ? typeof(Barracks) : typeof(Archery);
                // var botBuildingType = typeof(Archery);
                var botBuildingType = typeof(Barracks);
                _buildingSpawner.SpawnMobBuilding(botBuildingType, botTeam, buildingPlatform.transform.position);
                buildingPlatform.IsOccupied = true;
                
                Debug.Log($"Spawned {botBuildingType.Name} for Bots team");

                break;
            }
        }
    }
}