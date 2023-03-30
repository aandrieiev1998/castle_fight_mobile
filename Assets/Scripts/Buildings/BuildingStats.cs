using Mobs;
using Stats;
using UnityEngine;

namespace Buildings
{
    [CreateAssetMenu(menuName = "CFM/BuildingStats")]
    public class BuildingStats : BaseStats
    {
        public BuildingType _buildingType;
        public MobType _spawnedMob;
    }
}