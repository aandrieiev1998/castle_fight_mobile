using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildingMenuView : MonoBehaviour
    {
        [SerializeField] private Button _barrackButton;
        [SerializeField] private Button _archeryButton;
        [SerializeField] private Button _cancelButton;

        public Button BarrackButton => _barrackButton;

        public Button ArcheryButton => _archeryButton;

        public Button CancelButton => _cancelButton;
    }
}