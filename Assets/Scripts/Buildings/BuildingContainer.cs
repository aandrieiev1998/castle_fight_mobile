using System;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingContainer : MonoBehaviour
    {
        public List<BuildingBehaviour> _buildings;

        public event Action<BuildingBehaviour> NewBuilding;


        public void AddActiveBuilding(BuildingBehaviour buildingBehaviour)
        {
            _buildings.Add(buildingBehaviour);
            NewBuilding?.Invoke(buildingBehaviour);
        }
    }
}