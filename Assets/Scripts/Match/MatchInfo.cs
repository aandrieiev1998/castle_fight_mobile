using UnityEngine;

namespace Match
{
    public class MatchInfo : MonoBehaviour
    {

        [SerializeField] private PlayerTeam _localPlayerTeam;
        
        
        
        public PlayerTeam LocalPlayerTeam { get; set; }

    }
}