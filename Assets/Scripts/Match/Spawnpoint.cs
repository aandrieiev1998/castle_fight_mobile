using System;
using Buildings;
using Buildings.Types;
using UnityEngine;

namespace Match
{
    [Serializable]
    public struct Spawnpoint
    {
        public PlayerTeam _playerTeam;
        public BaseBuildingType _baseBuildingType;
        public Transform _transform;
    }
}