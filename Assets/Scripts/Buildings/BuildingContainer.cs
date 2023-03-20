using System;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingContainer : MonoBehaviour
    {

        public List<BuildingDefinition> _buildingPrefabs;
        public List<GameObject> _activeBuildings;
        
        public Action<BuildingDefinition, Vector3> newActiveBuilding;
        
        
        public void AddActiveBuilding(BuildingDefinition buildingDefinition, GameObject buildingOnScene)
        {
            _activeBuildings.Add(buildingOnScene);
            newActiveBuilding.Invoke(buildingDefinition, buildingOnScene.transform.position);
        }
        
    }
}