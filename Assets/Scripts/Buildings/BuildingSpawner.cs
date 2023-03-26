using System.Collections.Generic;
using System.Linq;
using Buildings.Definition;
using Buildings.Types;
using Match;
using UI;
using UnityEngine;

namespace Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private List<BaseBuildingDefinition> _baseBuildingDefinitions;
        [SerializeField] private List<MobBuildingDefinition> _mobBuildingDefinitions;
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private BuildingContainer _buildingContainer;
        [SerializeField] private SpawnPointsContainer _spawnPointsContainer;
        [SerializeField] private Transform _buildingsParent;
        [SerializeField] private BuildingsMenuController _buildingMenuController;
        [SerializeField] private TeamSelectionMenuController _teamSelectionMenuController;
        [SerializeField] private MatchInfo _matchInfo;
        [SerializeField] private List<TeamMaterial> _teamMaterials;
        [SerializeField] private GameObject _buildingPlatformPrefab;

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

        private void OnBuildingSelected(MobBuildingType baseBuildingType)
        {
            SpawnMobBuilding(baseBuildingType, _matchInfo.LocalPlayerTeam, spawnPoint);
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
            var spawnPoints = _spawnPointsContainer.MobBuildingsSpawnPoints.Where(bp => bp._playerTeam == playerTeam).ToList();
            foreach (var spawnPoint in spawnPoints)
            {
                var buildingPlatform = Instantiate(_buildingPlatformPrefab, spawnPoint._transform.position, Quaternion.identity);
                var buildingPlatformData = buildingPlatform.GetComponent<BuildingPlatform>();
                buildingPlatformData.PlayerTeam = playerTeam;
            }
        }

        private void SpawnBaseBuildings()
        {
            SpawnBaseBuilding(BaseBuildingType.Throne, PlayerTeam.Blue);
            SpawnBaseBuilding(BaseBuildingType.Throne, PlayerTeam.Red);
        }

        private void SpawnBaseBuilding(BaseBuildingType buildingType, PlayerTeam playerTeam)
        {
            var buildingDefinition = _baseBuildingDefinitions.Single(bdd => bdd._type == buildingType);
            var buildingSpawnPoint = _spawnPointsContainer.BaseBuildingSpawnPoints.Single(sp => sp._playerTeam == playerTeam);

            var building = Instantiate(buildingDefinition._prefab, buildingSpawnPoint._transform.position, buildingSpawnPoint._transform.rotation);
            var buildingData = building.GetComponent<BuildingData>();
            buildingData._playerTeam = playerTeam;
            var buildingRenderer = building.GetComponent<Renderer>();
            buildingRenderer.material = _teamMaterials.Single(tm => tm._playerTeam == playerTeam)._material;
        }

        public void SpawnMobBuilding(MobBuildingType baseBuildingType, PlayerTeam playerTeam, Vector3 position)
        {
            var buildingDefinition = _mobBuildingDefinitions.Single(definition => definition._type == baseBuildingType);
            var buildingPrefab = buildingDefinition._prefab;

            var building = Instantiate(buildingPrefab, position,
                Quaternion.Euler(new Vector3(-90f, 0f, 0f)), _buildingsParent);
            building.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            // building.

            var buildingData = building.AddComponent<BuildingData>();
            // buildingData._currentHp = buildingDefinition._mobStats._maxHp;
            // buildingData._currentArmor = buildingDefinition._mobStats._maxArmor;
            // buildingData._armorType = buildingDefinition._mobStats._armorType;
            buildingData._playerTeam = playerTeam;

            _buildingContainer.AddActiveBuilding(buildingDefinition, buildingData);

            // selectedPlatform.IsOccupied = true;
        }
    }
}