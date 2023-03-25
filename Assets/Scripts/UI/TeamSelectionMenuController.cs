using System;
using Match;
using UnityEngine;

namespace UI
{
    public class TeamSelectionMenuController : MonoBehaviour
    {

        [SerializeField] private TeamSelectionMenuView _teamSelectionMenuView;

        public event Action<PlayerTeam> PlayerTeamSelected;

        private void Start()
        {
            _teamSelectionMenuView.gameObject.SetActive(true);
            
            _teamSelectionMenuView.FistTeamButton.onClick.AddListener(() => {SelectTeam(PlayerTeam.Blue);});
            _teamSelectionMenuView.SecondTeamButton.onClick.AddListener(() => {SelectTeam(PlayerTeam.Red);});
        }

        private void SelectTeam(PlayerTeam team)
        {
            Debug.Log($"Selected team: {team}");
            PlayerTeamSelected?.Invoke(team);   
        }
        
        public void Hide()
        {
            _teamSelectionMenuView.gameObject.SetActive(false);
        }

        public void Show()
        {
            _teamSelectionMenuView.gameObject.SetActive(true);
        }
        
    }
}