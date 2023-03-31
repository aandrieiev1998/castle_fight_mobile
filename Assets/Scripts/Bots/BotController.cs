using System;
using System.Linq;
using Buildings;
using Match;
using UI;
using UnityEngine;

namespace Bots
{
    public class BotController : MonoBehaviour
    {
        [SerializeField] private TeamSelectionMenuController _teamSelectionMenuController;
        [SerializeField] private BuildingSpawner _buildingSpawner;
        [SerializeField] private SpawnPointsContainer _spawnPointsContainer;

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

            var mobBuildingsSpawnPoints =
                _spawnPointsContainer.MobBuildingsSpawnPoints.Where(sp => sp._teamColor == botTeam).ToList();
            foreach (var mobBuildingSpawnPoint in mobBuildingsSpawnPoints)
            {
                _buildingSpawner.SpawnBuilding(BuildingType.Barracks, botTeam,
                    mobBuildingSpawnPoint._transform.position);
            }

            Debug.Log(
                $"Spawned {mobBuildingsSpawnPoints.Count} buildings of type {BuildingType.Barracks}");
        }
    }
}