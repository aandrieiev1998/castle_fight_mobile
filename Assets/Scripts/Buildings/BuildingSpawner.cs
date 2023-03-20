using System;
using UnityEngine;

namespace Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject barrackPrefab;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private BuildingContainer buildingContainer;

        private bool _occupied;
        private int _layerIndex = 6;

        private void OnMouseDown()
        {
            Debug.Log(gameObject.name);
            if (!_occupied)
            {
                _occupied = true;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << _layerIndex))
                {
                    var building = Instantiate(barrackPrefab, hit.transform.position,
                        Quaternion.Euler(new Vector3(-90f, 0f, 0f)), transform);
                    building.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    
                    buildingContainer.AddActiveBuilding(buildingContainer._buildingPrefabs[0], building);
                }
            }
        }
    }
}