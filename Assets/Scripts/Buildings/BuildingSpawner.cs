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
        [SerializeField] private GameObject _buildingPlatformPrefab;
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private SpawnPointsContainer _spawnPointsContainer;
        [SerializeField] private Transform _buildingsParent;
        [SerializeField] private BuildingsMenuController _buildingMenuController;
        [SerializeField] private TeamSelectionMenuController _teamSelectionMenuController;
        [SerializeField] private MatchInfo _matchInfo;
        [SerializeField] private List<TeamMaterial> _teamMaterials;

        private const int LayerIndex = 6;
        private Vector3 spawnPoint;
        private BuildingPlatform selectedPlatform;
        private bool occupied;

        private void Start()
        {
            _buildingMenuController.BuildingSelected += OnBuildingSelected;
            _teamSelectionMenuController.PlayerTeamSelected += OnLocalPlayerTeamSelected;
        }

        private void OnLocalPlayerTeamSelected(TeamColor teamColor)
        {
            _teamSelectionMenuController.Hide();
            SpawnBuildingPlatforms(teamColor);
            SpawnBaseBuildings();
        }

        private void Update()
        {
            HandlePlayerInput();
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

        private void SpawnBuildingPlatforms(TeamColor teamColor)
        {
            var spawnPoints = _spawnPointsContainer.MobBuildingsSpawnPoints.Where(bp => bp._teamColor == teamColor)
                .ToList();
            foreach (var spawnPoint in spawnPoints)
            {
                var buildingPlatform = Instantiate(_buildingPlatformPrefab, spawnPoint._transform.position,
                    Quaternion.identity);
                var buildingPlatformData = buildingPlatform.GetComponent<BuildingPlatform>();
                buildingPlatformData.TeamColor = teamColor;
            }
        }

        private void SpawnBaseBuildings()
        {
            SpawnBuilding(typeof(Castle), TeamColor.Blue,
                _spawnPointsContainer.BaseBuildingSpawnPoints.Single(sp => sp._teamColor == TeamColor.Blue)._transform
                    .position);
            SpawnBuilding(typeof(Castle), TeamColor.Red,
                _spawnPointsContainer.BaseBuildingSpawnPoints.Single(sp => sp._teamColor == TeamColor.Red)._transform
                    .position);
        }

        public void SpawnBuilding(Type buildingType, TeamColor teamColor, Vector3 position)
        {
            var buildingPrefab = _buildingPrefabs.Single(bp => bp.GetType() == buildingType);

            var building = Instantiate(buildingPrefab, position,
                Quaternion.Euler(new Vector3(-90f, 0f, 0f)), _buildingsParent);
            building.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            var teamMaterial = _teamMaterials.Single(tm => tm._teamColor == teamColor);
            var buildingRenderer = building.GetComponent<Renderer>();
            buildingRenderer.material = teamMaterial._material;
            // selectedPlatform.IsOccupied = true;
        }
    }
}