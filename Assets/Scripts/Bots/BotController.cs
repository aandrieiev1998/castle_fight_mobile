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

        private void OnPlayerTeamSelected(PlayerTeam playerTeam)
        {
            var botTeam = playerTeam switch
            {
                PlayerTeam.Blue => PlayerTeam.Red,
                PlayerTeam.Red => PlayerTeam.Blue,
                _ => throw new Exception("Bot crashed")
            };

            var mobBuildingsSpawnPoints =
                _spawnPointsContainer.MobBuildingsSpawnPoints.Where(sp => sp._playerTeam == botTeam).ToList();
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