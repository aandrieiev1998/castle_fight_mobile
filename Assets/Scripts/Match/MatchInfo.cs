using UnityEngine;

namespace Match
{
    public class MatchInfo : MonoBehaviour
    {

        [SerializeField] private TeamColor _localTeamColor;
        [SerializeField] private int _teamsAmount;

        public int TeamsAmount
        {
            get => _teamsAmount;
            set => _teamsAmount = value;
        }

        public TeamColor LocalTeamColor { get; set; }

    }
}