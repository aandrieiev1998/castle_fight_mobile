using UnityEngine;

namespace GamePlay
{
    public class MatchInfo : MonoBehaviour
    {
        [SerializeField] private PlayerTeam localPlayerTeam;

        public PlayerTeam LocalPlayerTeam
        {
            get => localPlayerTeam;
            set => localPlayerTeam = value;
        }
    }
}