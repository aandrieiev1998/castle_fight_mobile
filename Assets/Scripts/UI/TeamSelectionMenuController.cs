using System;
using Match;
using UnityEngine;

namespace UI
{
    public class TeamSelectionMenuController : MonoBehaviour
    {

        [SerializeField] private TeamSelectionMenuView _teamSelectionMenuView;
        [SerializeField] private MatchInfo _matchInfo;

        public event Action<TeamColor> PlayerTeamSelected;

        private void Start()
        {
            _teamSelectionMenuView.gameObject.SetActive(true);
            
            _teamSelectionMenuView.FistTeamButton.onClick.AddListener(() => {SelectTeam(TeamColor.Blue);});
            _teamSelectionMenuView.SecondTeamButton.onClick.AddListener(() => {SelectTeam(TeamColor.Red);});
        }

        private void SelectTeam(TeamColor teamColor)
        {
            Debug.Log($"Selected team: {teamColor}");
            _matchInfo.LocalTeamColor = teamColor;
            PlayerTeamSelected?.Invoke(teamColor);   
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