using UI.Views;
using UnityEngine;

namespace Mobs
{
    public class MobInfoController : MonoBehaviour
    {
        [SerializeField] private UnitInfoView _unitInfoViewPrefab;
        private readonly Vector3 offset = new(0, 2.0f, 1.0f);
        private GUI_View guiView;
        private Camera mainCamera;
        private Transform mobTransform;
        private RectTransform rectTransform;

        private void Start()
        {
            guiView = FindObjectOfType<GUI_View>();
            mobTransform = GetComponent<Mob>().transform;
            if (Camera.main != null) mainCamera = Camera.main;

            var unitInfoView = Instantiate(_unitInfoViewPrefab, guiView.transform);
            rectTransform = unitInfoView.GetComponent<RectTransform>();
        }

        private void Update()
        {
            rectTransform.position = mainCamera.WorldToScreenPoint(mobTransform.position + offset);
        }
    }
}