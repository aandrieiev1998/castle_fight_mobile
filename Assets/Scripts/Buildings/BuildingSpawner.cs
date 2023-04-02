using System;
using System.Collections.Generic;
using System.Linq;
using Match;
using UI;
using UnityEngine;

namespace Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        private const int LayerIndex = 6;
        [SerializeField] private List<MobBuilding> _mobBuildingPrefabs;
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private BuildingsMenuController _buildingMenuController;
        [SerializeField] private TeamSelectionMenuController _teamSelectionMenuController;
        [SerializeField] private MatchInfo _matchInfo;
        private bool occupied;
        private BuildingPlatform selectedPlatform;
        private Vector3 spawnPoint;

        private void Start()
        {
            _teamSelectionMenuController.PlayerTeamSelected += OnLocalPlayerTeamSelected;
            _buildingMenuController.BuildingSelected += OnBuildingSelected;
        }

        private void Update()
        {
            HandlePlayerInput();
        }

        private void OnLocalPlayerTeamSelected(TeamColor teamColor)
        {
            _teamSelectionMenuController.Hide();
            _matchInfo.MatchStarted = true;

            var buildingPlatforms = FindObjectsOfType<BuildingPlatform>();
            foreach (var buildingPlatform in buildingPlatforms)
                if (buildingPlatform.TeamColor != _matchInfo.LocalTeamColor)
                    buildingPlatform.GetComponent<MeshRenderer>().enabled = false;
        }

        private void OnBuildingSelected(Type buildingType)
        {
            SpawnMobBuilding(buildingType, _matchInfo.LocalTeamColor, spawnPoint);

            selectedPlatform.IsOccupied = true;

            _buildingMenuController.Hide();
        }

        private void HandlePlayerInput()
        {
            if (_matchInfo.MatchStarted == false) return;

            if (Input.GetMouseButtonDown(0))
            {
                var ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerIndex))
                {
                    var targetPlatform = hit.transform.GetComponent<BuildingPlatform>();
                    if (targetPlatform == null) return;
                    
                    if (!targetPlatform.IsOccupied && targetPlatform.TeamColor == _matchInfo.LocalTeamColor)
                    {
                        _buildingMenuController.Show();
                        spawnPoint = hit.transform.position;
                        selectedPlatform = targetPlatform;
                    }
                    else
                    {
                        Debug.Log("Is occupied or is in control of other team");
                    }
                }
            }
        }

        public void SpawnMobBuilding(Type buildingType, TeamColor teamColor, Vector3 position)
        {
            var buildingPrefab = _mobBuildingPrefabs.Single(bp => bp.GetType() == buildingType);

            var building = Instantiate(buildingPrefab, position,
                Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
            building.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            building.TeamColor = teamColor;

            building.SpawnMobs();
        }
    }
}