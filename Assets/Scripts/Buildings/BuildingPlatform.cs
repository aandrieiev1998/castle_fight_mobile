using UnityEngine;
using GamePlay;

namespace Buildings
{
    public class BuildingPlatform : MonoBehaviour
    {
        [SerializeField] private PlayerTeam _playerTeam;
        [SerializeField] private bool _isOccupied;
        
        
        public bool IsOccupied
        {
            get => _isOccupied;
            set => _isOccupied = value;
        }

        public PlayerTeam PlayerTeam
        {
            get => _playerTeam;
            set => _playerTeam = value;
        }
    }
}