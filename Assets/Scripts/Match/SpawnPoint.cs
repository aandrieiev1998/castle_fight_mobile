using System;
using UnityEngine;

namespace Match
{
    [Serializable]
    public struct SpawnPoint
    {
        public PlayerTeam _playerTeam;
        public Transform _transform;
    }
}