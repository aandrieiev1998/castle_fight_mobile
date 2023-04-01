using System;
using Buildings;
using Buildings.MobBuildings;
using Match;
using UI;
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
                _ => throw new Exception("Bot crashed")
            };

            var buildingPlatforms = FindObjectsOfType<BuildingPlatform>();
            foreach (var buildingPlatform in buildingPlatforms)
            {
                if (buildingPlatform.TeamColor != botTeam || buildingPlatform.IsOccupied) continue;

                var botBuildingType = Random.value > 0.5f ? typeof(Barracks) : typeof(Archery);
                _buildingSpawner.SpawnMobBuilding(botBuildingType, botTeam, buildingPlatform.transform.position);
                buildingPlatform.IsOccupied = true;
            }

            Debug.Log(
                $"Spawned {buildingPlatforms.Length} by for bot team");
        }
    }
}