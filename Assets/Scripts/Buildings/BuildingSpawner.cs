using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private List<BuildingDefinition> _buildingDefinitions;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private BuildingContainer buildingContainer;
        [SerializeField] private Transform buildingsParent;

        private bool _occupied;
        private int _layerIndex = 6;

        private void Start()
        {
            // TODO set buildingsParent to be dependent on selected team
        }

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
                    // todo get selected building from UI

                    var buildingDefinition = _buildingDefinitions[0];
                    var buildingPrefab = buildingDefinition._prefab;
                    
                    var building = Instantiate(buildingPrefab, hit.transform.position,
                        Quaternion.Euler(new Vector3(-90f, 0f, 0f)), buildingsParent);
                    building.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    
                    var buildingData = building.AddComponent<BuildingData>();
                    buildingData._currentHp = buildingDefinition._stats._maxHp;
                    
                    buildingContainer.AddActiveBuilding(buildingDefinition, buildingData);
                }
            }
        }
    }
}