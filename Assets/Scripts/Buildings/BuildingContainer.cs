using System;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingContainer : MonoBehaviour
    {
        public List<BuildingData> _activeBuildings;

        public event Action<BuildingDefinition, Vector3> NewActiveBuilding;


        public void AddActiveBuilding(BuildingDefinition buildingDefinition, BuildingData buildingData)
        {
            _activeBuildings.Add(buildingData);
            NewActiveBuilding?.Invoke(buildingDefinition, buildingData.transform.position);
        }
    }
}