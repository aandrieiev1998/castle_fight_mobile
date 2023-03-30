using System;
using Buildings;
using UnityEngine;

namespace UI
{
    public class BuildingsMenuController : MonoBehaviour
    {
        [SerializeField] private BuildingMenuView _buildingMenuView;

        public event Action<BuildingType> BuildingSelected;

        private void Start()
        {
            _buildingMenuView.gameObject.SetActive(true);
            
            _buildingMenuView.BarrackButton.onClick.AddListener(() => {SelectMobBuilding(BuildingType.Barracks);});
            _buildingMenuView.ArcheryButton.onClick.AddListener(() => {SelectMobBuilding(BuildingType.Archery);});
            
            _buildingMenuView.CancelButton.onClick.AddListener(Hide);
            
            _buildingMenuView.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _buildingMenuView.BarrackButton.onClick.RemoveAllListeners();
            _buildingMenuView.ArcheryButton.onClick.RemoveAllListeners();
            
            _buildingMenuView.CancelButton.onClick.RemoveListener(Hide);
        }

        private void SelectMobBuilding(BuildingType buildingType)
        {
            Debug.Log($"Selected building: {buildingType}");
            BuildingSelected?.Invoke(buildingType);
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