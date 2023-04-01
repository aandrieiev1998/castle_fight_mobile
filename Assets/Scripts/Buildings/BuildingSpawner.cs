using System;
using System.Collections.Generic;
using System.Linq;
using Match;
using Scripts3.Buildings;
using UI;
using UnityEngine;

namespace Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private List<Building> _buildingPrefabs;
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private BuildingsMenuController _buildingMenuController;
        [SerializeField] private TeamSelectionMenuController _teamSelectionMenuController;
        [SerializeField] private MatchInfo _matchInfo;

        private const int LayerIndex = 6;
        private Vector3 spawnPoint;
        private BuildingPlatform selectedPlatform;
        private bool occupied;

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

            var buildingPlatforms = FindObjectsOfType<BuildingPlatform>();
            foreach (var buildingPlatform in buildingPlatforms)
            {
                if (buildingPlatform.TeamColor != _matchInfo.LocalTeamColor)
                {
                    buildingPlatform.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }

        private void OnBuildingSelected(Type buildingType)
        {
            SpawnBuilding(buildingType, _matchInfo.LocalTeamColor, spawnPoint);
            _buildingMenuController.Hide();
        }

        private void HandlePlayerInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerIndex))
                {
                    selectedPlatform = hit.transform.GetComponent<BuildingPlatform>();
                    if (!selectedPlatform.IsOccupied)
                    {
                        _buildingMenuController.Show();
                        spawnPoint = hit.transform.position;
                    }
                    else
                    {
                        Debug.Log("Is occupied");
                    }
                }
            }
        }

        public void SpawnBuilding(Type buildingType, TeamColor teamColor, Vector3 position)
        {
            var buildingPrefab = _buildingPrefabs.Single(bp => bp.GetType() == buildingType);

            var building = Instantiate(buildingPrefab, position,
                Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
            building.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            building.TeamColor = teamColor;
            
            selectedPlatform.IsOccupied = true;
        }
    }
}