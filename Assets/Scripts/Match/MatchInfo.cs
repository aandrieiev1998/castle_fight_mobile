using UnityEngine;

namespace Match
{
    public class MatchInfo : MonoBehaviour
    {

        [SerializeField] private PlayerTeam _localPlayerTeam;
        [SerializeField] private int _teamsAmount;

        public int TeamsAmount
        {
            get => _teamsAmount;
            set => _teamsAmount = value;
        }

        public PlayerTeam LocalPlayerTeam { get; set; }

    }
}