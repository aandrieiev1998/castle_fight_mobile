using System;
using Match;
using UnityEngine;

namespace Systems.Impl
{
    [Serializable]
    public class TeamSystem : ITeamSystem
    {
        [SerializeField] private TeamColor _teamColor;

        public TeamColor TeamColor
        {
            get => _teamColor;
            set => _teamColor = value;
        }
    }
}