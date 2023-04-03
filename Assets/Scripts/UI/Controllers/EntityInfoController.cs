using Entities;
using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class EntityInfoController : MonoBehaviour
    {
        [SerializeField] private UnitInfoView _unitInfoViewPrefab;
        [SerializeField] private Vector3 _offset = new(0, 2.0f, 1.0f);
        private UnitInfoView unitInfoView;
        private GUI_View guiView;
        private Camera mainCamera;
        private GameEntity entity;
        private RectTransform rectTransform;

        private void Start()
        {
            guiView = FindObjectOfType<GUI_View>();
            entity = GetComponent<GameEntity>();
            if (Camera.main != null) mainCamera = Camera.main;

            unitInfoView = Instantiate(_unitInfoViewPrefab, guiView.transform);
            rectTransform = unitInfoView.GetComponent<RectTransform>();

            unitInfoView.HealthSlider.maxValue = entity.HealthSystem.HealthAmount;
            unitInfoView.HealthSlider.value = entity.HealthSystem.HealthAmount;
            unitInfoView.HealthSlider.minValue = 0;
        }

        private void Update()
        {
            rectTransform.position = mainCamera.WorldToScreenPoint(entity.transform.position + _offset);

            unitInfoView.HpText.SetText(string.Format(unitInfoView.hpTemplate,
                entity.HealthSystem.HealthAmount.ToString("N0")));
            unitInfoView.ArmorText.SetText(string.Format(unitInfoView.armorTemplate,
                entity.HealthSystem.ArmorAmount.ToString("N0")));
        }

        private void OnDestroy()
        {
            if (unitInfoView != null)
                Destroy(unitInfoView.gameObject);
        }
    }
}