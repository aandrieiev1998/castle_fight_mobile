using Match;
using UnityEngine;

namespace Buildings
{
    public class BuildingPlatform : MonoBehaviour
    {
        [SerializeField] private TeamColor _teamColor;
        [SerializeField] private bool _isOccupied;
        
        
        public bool IsOccupied
        {
            get => _isOccupied;
            set => _isOccupied = value;
        }

        public TeamColor TeamColor
        {
            get => _teamColor;
            set => _teamColor = value;
        }
    }
}