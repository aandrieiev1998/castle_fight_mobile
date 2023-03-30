using Scripts2.Stats;
using Sirenix.Utilities;
using UnityEngine;

namespace Scripts2.Buildings
{
    public class BuildingBehaviour : MonoBehaviour
    {
        public BuildingStats _buildingStats;
        public BuildingData _buildingData;
        
        private void Start()
        {
            _buildingStats.Stats.ForEach(pair =>
                _buildingData._activeStats.Add(new ActiveStat(pair.Key, pair.Value, pair.Value)));
        }
    }
}