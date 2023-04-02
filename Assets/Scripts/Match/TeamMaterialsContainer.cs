using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match
{
    public class TeamMaterialsContainer : MonoBehaviour
    {
        [SerializeField] private List<TeamMaterial> _buildingMaterials;
        [SerializeField] private List<TeamMaterial> _mobMaterials;

        public Dictionary<TeamColor, Material> BuildingMaterials { get; } = new();
        public Dictionary<TeamColor, Material> MobMaterials { get; } = new();

        private void Awake()
        {
            foreach (var buildingMaterial in _buildingMaterials)
                BuildingMaterials.Add(buildingMaterial._teamColor, buildingMaterial._material);
            foreach (var mobMaterial in _mobMaterials) MobMaterials.Add(mobMaterial._teamColor, mobMaterial._material);
        }
    }

    [Serializable]
    public struct TeamMaterial
    {
        public TeamColor _teamColor;
        public Material _material;
    }
}