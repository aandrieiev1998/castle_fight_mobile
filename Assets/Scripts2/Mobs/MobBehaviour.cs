using System.Collections.Generic;
using Mobs;
using Scripts2.Data;
using Scripts2.Stats;
using UnityEngine;
using MobStats = Scripts2.Stats.MobStats;

namespace Scripts2.Mobs
{
    public class MobBehaviour : MonoBehaviour
    {
        public MobType _type;
        public MobStats _mobStats;
        public Dictionary<StatType, BaseData> mobData;
        public PathfindingParameters _pathfindingParameters;

        private void Start()
        {
            // _mobStats.Stats TODO
        }
    }
}