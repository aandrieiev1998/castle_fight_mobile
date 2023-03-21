﻿using System;
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
            
            _buildingMenuView.BarrackButton.onClick.AddListener(() => {SelectBuilding(BuildingType.Barracks);});
            _buildingMenuView.ArcheryButton.onClick.AddListener(() => {SelectBuilding(BuildingType.Archery);});
            _buildingMenuView.CancelButton.onClick.AddListener(Hide);
            
            _buildingMenuView.gameObject.SetActive(false);
        }

        private void SelectBuilding(BuildingType buildingType)
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