using System;
using System.Collections.Generic;
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

        private const int LayerIndex = 6;
        private Vector3 spawnPoint;
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
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerIndex))
                {
                    _buildingMenuController.Show();
                    spawnPoint = hit.transform.position;
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
            var buildingDefinition = _buildingDefinitions[(int)buildingType];
            var buildingPrefab = buildingDefinition._prefab;

            var building = Instantiate(buildingPrefab, position,
                Quaternion.Euler(new Vector3(-90f, 0f, 0f)), _buildingsParent);
            building.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            var buildingData = building.AddComponent<BuildingData>();
            buildingData._currentHp = buildingDefinition._stats._maxHp;

            _buildingContainer.AddActiveBuilding(buildingDefinition, buildingData);
        }
    }
}