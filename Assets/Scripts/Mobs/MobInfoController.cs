using UI.Views;
using UnityEngine;

namespace Mobs
{
    public class MobInfoController : MonoBehaviour
    {
        [SerializeField] private UnitInfoView _unitInfoViewPrefab;
        private readonly Vector3 offset = new(0, 2.0f, 1.0f);
        private UnitInfoView unitInfoView;
        private GUI_View guiView;
        private Camera mainCamera;
        private Mob mob;
        private RectTransform rectTransform;

        private void Start()
        {
            guiView = FindObjectOfType<GUI_View>();
            mob = GetComponent<Mob>();
            if (Camera.main != null) mainCamera = Camera.main;

            unitInfoView = Instantiate(_unitInfoViewPrefab, guiView.transform);
            rectTransform = unitInfoView.GetComponent<RectTransform>();
        }

        private void Update()
        {
            rectTransform.position = mainCamera.WorldToScreenPoint(mob.transform.position + offset);

            unitInfoView.HpText.SetText(string.Format(unitInfoView.hpTemplate, mob.HealthSystem.HealthAmount));
            unitInfoView.ArmorText.SetText(string.Format(unitInfoView.armorTemplate, mob.HealthSystem.ArmorAmount));
        }
    }
}