using Mobs;
using Scripts2.Stats;
using Sirenix.Utilities;
using UnityEngine;
using MobData = Scripts2.Data.MobData;
using MobStats = Scripts2.Stats.MobStats;

namespace Scripts2.Mobs
{
    public class MobBehaviour : MonoBehaviour
    {
        public MobStats _mobStats;
        public MobData _mobData;
        public PathfindingParameters _pathfindingParameters; // todo refactor

        private void Start()
        {
            _mobStats.Stats.ForEach(pair =>
                _mobData._activeStats.Add(new ActiveStat(pair.Key, pair.Value, pair.Value)));
        }
    }
}