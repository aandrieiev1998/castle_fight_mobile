using Sirenix.Utilities;
using Stats;
using UnityEngine;

namespace Buildings
{
    public class BuildingBehaviour : MonoBehaviour
    {
        public BuildingStats _buildingStats;
        public BuildingData _buildingData;
        
        private void Awake()
        {
            _buildingStats.Stats.ForEach(pair =>
                _buildingData.activeStats.Add(pair.Key, new ActiveStat(pair.Key, pair.Value, pair.Value)));
            _buildingData._buildingType = _buildingStats._buildingType;
        }
    }
}