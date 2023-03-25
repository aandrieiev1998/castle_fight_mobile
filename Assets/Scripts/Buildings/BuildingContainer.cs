using System;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingContainer : MonoBehaviour
    {
        public List<BuildingData> _activeBuildings;

        public event Action<MobBuildingDefinition, Vector3> NewActiveBuilding;


        public void AddActiveBuilding(MobBuildingDefinition mobBuildingDefinition, BuildingData buildingData)
        {
            _activeBuildings.Add(buildingData);
            NewActiveBuilding?.Invoke(mobBuildingDefinition, buildingData.transform.position);
        }
    }
}