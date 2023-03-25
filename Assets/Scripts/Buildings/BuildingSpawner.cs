using System.Collections.Generic;
using System.Linq;
using Match;
using UI;
using UnityEngine;

namespace Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private List<BaseBuildingDefinition> _baseBuildingDefinitions;
        [SerializeField] private List<MobBuildingDefinition> _buildingDefinitions;
        [SerializeField] private UnityEngine.Camera _playerCamera;
        [SerializeField] private BuildingContainer _buildingContainer;
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
        }

        private void Update()
        {
            HandlePlayerInput();
        }

        private void OnBuildingSelected(BuildingType buildingType)
        {
            SpawnBuilding(buildingType, spawnPoint);
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

        private void SpawnBuilding(BuildingType buildingType, Vector3 position)
        {
            var buildingDefinition = _buildingDefinitions.Single(definition => definition._type == buildingType);
            var buildingPrefab = buildingDefinition._prefab;

            var building = Instantiate(buildingPrefab, position,
                Quaternion.Euler(new Vector3(-90f, 0f, 0f)), _buildingsParent);
            building.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            
            // building.

            var buildingData = building.AddComponent<BuildingData>();
            buildingData._currentHp = buildingDefinition._mobStats._maxHp;
            buildingData._currentArmor = buildingDefinition._mobStats._maxArmor;
            buildingData._armorType = buildingDefinition._mobStats._armorType;
            buildingData._PlayerTeam = _matchInfo.LocalPlayerTeam;

            _buildingContainer.AddActiveBuilding(buildingDefinition, buildingData);

            selectedPlatform.IsOccupied = true;
        }
    }
}