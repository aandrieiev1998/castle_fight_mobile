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
        public float _aiRadius;
        public float _aiHeight;
        public float _aiMaxSpeed;
    }
}