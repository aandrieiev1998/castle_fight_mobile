using System;
using Buildings;
using UnityEngine;

namespace Match
{
    [Serializable]
    public struct Spawnpoint
    {
        public PlayerTeam _playerTeam;
        public BuildingType _buildingType;
        public Transform _transform;
    }
}