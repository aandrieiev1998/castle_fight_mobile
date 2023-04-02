using System;
using Match;
using Mechanics;
using Systems;
using UnityEngine;

namespace Buildings
{
    public abstract class Building : MonoBehaviour, IHealthSystem, ITeamSystem
    {
        [SerializeField] private float _healthAmount;
        [SerializeField] private float _healthRegen;
        [SerializeField] private float _armor;
        [SerializeField] private ArmorType _armorType;
        [SerializeField] private TeamColor _teamColor;

        private void Start()
        {
            var teamMaterialsContainer = FindObjectOfType<TeamMaterialsContainer>();
            var buildingRenderer = GetComponent<Renderer>();
            buildingRenderer.material = teamMaterialsContainer.BuildingMaterials[_teamColor];
        }

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

        public void ReceiveDamage(DamageType damageType, float damageAmount)
        {
            var damagePercentage = DamageUtils.GetDamagePercentage(ArmorType, damageType);
            var damageReduced = damageAmount * damagePercentage *
                                (1.0f - 0.06f * Armor / (1.0f + 0.06f * Math.Abs(Armor)));
            HealthAmount -= damageReduced;

            if (HealthAmount <= 0f)
            {
                Debug.Log($"{gameObject.name} has died");
                Destroy(gameObject);
            }
        }

        public TeamColor TeamColor
        {
            get => _teamColor;
            set => _teamColor = value;
        }
    }
}