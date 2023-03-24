using System;
using UnityEngine;

namespace Mobs
{
    [Serializable]
    public struct MobDefinition
    {
        public MobType _type;
        public MobStats _stats;
        public GameObject _prefab;
        public PathfindingParameters _pathfindingParameters;
    }
}