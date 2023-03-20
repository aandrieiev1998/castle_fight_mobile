using System;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingContainer : MonoBehaviour
    {

        public List<BuildingEntry> _buildingPrefabs;
        public List<GameObject> _activeBuildings;
        
        public Action<BuildingEntry, Vector3> newActiveBuilding;
        
        
        public void AddActiveBuilding(BuildingEntry buildingEntry, GameObject buildingOnScene)
        {
            _activeBuildings.Add(buildingOnScene);
            newActiveBuilding.Invoke(buildingEntry, buildingOnScene.transform.position);
        }
        
    }
}