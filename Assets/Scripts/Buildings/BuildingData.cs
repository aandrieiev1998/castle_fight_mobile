using System;
using System.Collections.Generic;
using Match;
using Stats;

namespace Buildings
{
    [Serializable]
    public class BuildingData 
    {
        public BuildingType _buildingType;
        public TeamColor _teamColor;
        public Dictionary<StatType, ActiveStat> activeStats = new();
    }
}