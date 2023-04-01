using Match;
using Scripts3.Mechanics;
using Scripts3.Systems;
using UnityEngine;

namespace Scripts3.Buildings
{
    public abstract class Building : MonoBehaviour, IHealthSystem, ITeamSystem
    {
        [SerializeField] private float _healthAmount;
        [SerializeField] private float _healthRegen;
        [SerializeField] private float _armor;
        [SerializeField] private ArmorType _armorType;
        [SerializeField] private TeamColor _teamColor;

        public float HealthAmount
        {
            get => _healthAmount;
            set => _healthAmount = value;
        }

        public float HealthRegen
        {
            get => _healthRegen;
            set => _healthRegen = value;
        }

        public float Armor
        {
            get => _armor;
            set => _armor = value;
        }

        public ArmorType ArmorType
        {
            get => _armorType;
            set => _armorType = value;
        }

        public TeamColor TeamColor
        {
            get => _teamColor;
            set => _teamColor = value;
        }

        private void Start()
        {
            var teamMaterialsContainer = FindObjectOfType<TeamMaterialsContainer>();
            var buildingRenderer = GetComponent<Renderer>();
            buildingRenderer.material = teamMaterialsContainer.BuildingMaterials[_teamColor];
        }
    }
}