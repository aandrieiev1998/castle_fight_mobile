using System.Collections.Generic;
using UnityEngine;

namespace Match
{
    public static class TeamMaterials
    {
        public static readonly Dictionary<TeamColor, Material> BuildingMaterials = new();
        public static readonly Dictionary<TeamColor, Material> MobMaterials = new();

        static TeamMaterials()
        {
            var buildingMaterialBlue = Resources.Load("Materials/Buildings_blue.mat", typeof(Material)) as Material;
            var buildingMaterialRed = Resources.Load("Materials/Buildings_red.mat", typeof(Material)) as Material;
            BuildingMaterials.Add(TeamColor.Blue, buildingMaterialBlue);
            BuildingMaterials.Add(TeamColor.Red, buildingMaterialRed);

            var mobMaterialBlue = Resources.Load("Materials/Mobs_blue.mat", typeof(Material)) as Material;
            var mobMaterialRed = Resources.Load("Materials/Mobs_red.mat", typeof(Material)) as Material;
            MobMaterials.Add(TeamColor.Blue, mobMaterialBlue);
            MobMaterials.Add(TeamColor.Red, mobMaterialRed);
        }
    }
}