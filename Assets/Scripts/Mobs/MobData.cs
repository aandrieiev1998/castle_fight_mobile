using System;
using System.Collections.Generic;
using Match;
using Mechanics;
using Stats;
using UnityEngine;

namespace Mobs
{
    [Serializable]
    public class MobData
    {
        public MobType _mobType;
        public ArmorType _armorType;
        public PlayerTeam _playerTeam;
        public Dictionary<StatType, ActiveStat> activeStats = new();
    }
}