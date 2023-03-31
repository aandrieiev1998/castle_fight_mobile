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
        public TeamColor _teamColor;
        public Dictionary<StatType, ActiveStat> activeStats = new();
    }
}