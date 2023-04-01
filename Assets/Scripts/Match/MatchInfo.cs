using UnityEngine;

namespace Match
{
    public class MatchInfo : MonoBehaviour
    {

        [SerializeField] private TeamColor _localTeamColor;

        public TeamColor LocalTeamColor { get; set; }

    }
}