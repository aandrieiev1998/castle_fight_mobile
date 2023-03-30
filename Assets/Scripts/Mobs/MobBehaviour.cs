using Sirenix.Utilities;
using Stats;
using UnityEngine;

namespace Mobs
{
    public class MobBehaviour : MonoBehaviour
    {
        public MobStats _mobStats;
        public MobData _mobData;
        public PathfindingParameters _pathfindingParameters; // todo refactor

        private void Awake()
        {
            _mobStats.Stats.ForEach(pair =>
                _mobData.activeStats.Add(pair.Key, new ActiveStat(pair.Key, pair.Value, pair.Value)));
            _mobData._mobType = _mobStats._mobType;
        }
    }
}