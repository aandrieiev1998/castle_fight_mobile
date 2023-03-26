using System;
using Buildings;
using Buildings.Types;
using UnityEngine;

namespace UI
{
    public class BuildingsMenuController : MonoBehaviour
    {
        [SerializeField] private BuildingMenuView _buildingMenuView;

        public event Action<MobBuildingType> BuildingSelected;

        private void Start()
        {
            _buildingMenuView.gameObject.SetActive(true);
            
            _buildingMenuView.BarrackButton.onClick.AddListener(() => {SelectMobBuilding(MobBuildingType.Barracks);});
            _buildingMenuView.ArcheryButton.onClick.AddListener(() => {SelectMobBuilding(MobBuildingType.Archery);});
            
            _buildingMenuView.CancelButton.onClick.AddListener(Hide);
            
            _buildingMenuView.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _buildingMenuView.BarrackButton.onClick.RemoveAllListeners();
            _buildingMenuView.ArcheryButton.onClick.RemoveAllListeners();
            
            _buildingMenuView.CancelButton.onClick.RemoveListener(Hide);
        }

        private void SelectMobBuilding(MobBuildingType baseBuildingType)
        {
            Debug.Log($"Selected building: {baseBuildingType}");
            BuildingSelected?.Invoke(baseBuildingType);
        }

        public void Hide()
        {
            _buildingMenuView.gameObject.SetActive(false);
        }

        public void Show()
        {
            _buildingMenuView.gameObject.SetActive(true);
        }
    }
}