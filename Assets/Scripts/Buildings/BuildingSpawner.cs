using System.Collections.Generic;
using System.Linq;
using Match;
using UI;
using UnityEngine;

namespace Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _buildingPrefabs;
        [SerializeField] private GameObject _buildingPlatformPrefab;
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private BuildingContainer _buildingContainer;
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

        private void OnLocalPlayerTeamSelected(PlayerTeam playerTeam)
        {
            _teamSelectionMenuController.Hide();
            SpawnBuildingPlatforms(playerTeam);
            SpawnBaseBuildings();
        }

        private void Update()
        {
            HandlePlayerInput();
        }

        private void OnBuildingSelected(BuildingType buildingType)
        {
            SpawnBuilding(buildingType, _matchInfo.LocalPlayerTeam, spawnPoint);
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

        private void SpawnBuildingPlatforms(PlayerTeam playerTeam)
        {
            var spawnPoints = _spawnPointsContainer.MobBuildingsSpawnPoints.Where(bp => bp._playerTeam == playerTeam)
                .ToList();
            foreach (var spawnPoint in spawnPoints)
            {
                var buildingPlatform = Instantiate(_buildingPlatformPrefab, spawnPoint._transform.position,
                    Quaternion.identity);
                var buildingPlatformData = buildingPlatform.GetComponent<BuildingPlatform>();
                buildingPlatformData.PlayerTeam = playerTeam;
            }
        }

        private void SpawnBaseBuildings()
        {
            SpawnBuilding(BuildingType.Throne, PlayerTeam.Blue,
                _spawnPointsContainer.BaseBuildingSpawnPoints.Single(sp => sp._playerTeam == PlayerTeam.Blue)._transform
                    .position);
            SpawnBuilding(BuildingType.Throne, PlayerTeam.Red,
                _spawnPointsContainer.BaseBuildingSpawnPoints.Single(sp => sp._playerTeam == PlayerTeam.Red)._transform
                    .position);
        }

        public void SpawnBuilding(BuildingType buildingType, PlayerTeam playerTeam, Vector3 position)
        {
            var buildingPrefab = _buildingPrefabs.Single(prefab =>
                prefab.GetComponent<BuildingBehaviour>()._buildingStats._buildingType == buildingType);

            var building = Instantiate(buildingPrefab, position,
                Quaternion.Euler(new Vector3(-90f, 0f, 0f)), _buildingsParent);
            building.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            var teamMaterial = _teamMaterials.Single(tm => tm._playerTeam == playerTeam);
            var buildingRenderer = building.GetComponent<Renderer>();
            buildingRenderer.material = teamMaterial._material;

            var buildingBehaviour = building.GetComponent<BuildingBehaviour>();
            var buildingData = buildingBehaviour._buildingData;
            buildingData._playerTeam = playerTeam;

            _buildingContainer.AddActiveBuilding(buildingBehaviour);

            // selectedPlatform.IsOccupied = true;
        }
    }
}