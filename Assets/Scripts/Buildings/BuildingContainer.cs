using System;
using System.Collections.Generic;
using Buildings.Data;
using Buildings.Definition;
using UnityEngine;

namespace Buildings
{
    public class BuildingContainer : MonoBehaviour
    {
        public List<BaseBuildingData> _baseBuildings;
        public List<MobBuildingData> _mobBuildings;

        public event Action<MobBuildingDefinition, MobBuildingData> NewMobBuilding;


        public void AddActiveBuilding(MobBuildingDefinition mobBuildingDefinition, MobBuildingData buildingData)
        {
            _mobBuildings.Add(buildingData);
            NewMobBuilding?.Invoke(mobBuildingDefinition, buildingData);
        }

        public void AddBaseBuilding(BaseBuildingDefinition baseBuildingDefinition, BaseBuildingData buildingData)
        {
            _baseBuildings.Add(buildingData);
        }
    }
}