using Match;
using UnityEngine;

namespace Entities.Buildings
{
    public abstract class Building : GameEntity
    {
        public virtual void Start()
        {
            Debug.Log("Building start method");
            var teamMaterialsContainer = FindObjectOfType<TeamMaterialsContainer>();
            var buildingRenderer = GetComponentInChildren<Renderer>();
            buildingRenderer.material = teamMaterialsContainer.BuildingMaterials[TeamSystem.TeamColor];
        }
    }
}