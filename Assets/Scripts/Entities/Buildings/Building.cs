using Match;
using UnityEngine;

namespace Entities.Buildings
{
    public abstract class Building : GameEntity
    {
        public virtual void Start()
        {
            var teamMaterialsContainer = FindObjectOfType<TeamMaterialsContainer>();
            var buildingRenderer = GetComponentInChildren<Renderer>();
            buildingRenderer.material = teamMaterialsContainer.BuildingMaterials[TeamSystem.TeamColor];
        }
    }
}