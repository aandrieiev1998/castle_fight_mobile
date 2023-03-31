using System;
using UnityEngine;

namespace Match
{
    [Serializable]
    public struct SpawnPoint
    {
        public TeamColor _teamColor;
        public Transform _transform;
    }
}