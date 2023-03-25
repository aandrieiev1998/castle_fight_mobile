using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TeamSelectionMenuView : MonoBehaviour
    {

        [SerializeField] private Button _fistTeamButton;
        [SerializeField] private Button _secondTeamButton;

        public Button FistTeamButton
        {
            get => _fistTeamButton;
            set => _fistTeamButton = value;
        }

        public Button SecondTeamButton
        {
            get => _secondTeamButton;
            set => _secondTeamButton = value;
        }
    }
}