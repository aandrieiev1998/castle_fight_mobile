using System;
using Match;
using UnityEngine;

namespace Systems
{
    [Serializable]
    public class TeamSystem
    {
        [SerializeField] private TeamColor _teamColor;

        public TeamColor TeamColor
        {
            get => _teamColor;
            set => _teamColor = value;
        }
    }
}