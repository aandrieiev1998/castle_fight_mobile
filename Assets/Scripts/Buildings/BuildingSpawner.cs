using System.Collections.Generic;
using GamePlay;
using UI;
using UnityEngine;

namespace Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private List<BuildingDefinition> _buildingDefinitions;
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private BuildingContainer _buildingContainer;
        [SerializeField] private Transform _buildingsParent;
        [SerializeField] private BuildingsMenuController _buildingMenuController;
        [SerializeField] private MatchInfo _matchInfo; 
        
        private const int LayerIndex = 6;
        private Vector3 spawnPoint;
        private BuildingPlatform selectedPlatform;
        private bool occupied;

        private void Start()
        {
            _buildingMenuController.BuildingSelected += OnBuildingSelected;
        }

        private void OnMouseDown()
        {
            Debug.Log(gameObject.name);
            if (!occupied)
            {
                occupied = true;
            }
        }

        private void Update()
        {
            HandlePlayerInput();
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

        private void OnBuildingSelected(BuildingType buildingType)
        {
            SpawnBuilding(buildingType, spawnPoint);
            _buildingMenuController.Hide();
        }

        private void SpawnBuilding(BuildingType buildingType, Vector3 position)
        {
            var buildingDefinition = _buildingDefinitions[(int) buildingType];
            var buildingPrefab = buildingDefinition._prefab;

            var building = Instantiate(buildingPrefab, position,
                Quaternion.Euler(new Vector3(-90f, 0f, 0f)), _buildingsParent);
            building.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            var buildingData = building.AddComponent<BuildingData>();
            buildingData._currentHp = buildingDefinition._stats._maxHp;
            buildingData._PlayerTeam = _matchInfo.LocalPlayerTeam;

            _buildingContainer.AddActiveBuilding(buildingDefinition, buildingData);
            
            selectedPlatform.IsOccupied = true;
        }
    }
}