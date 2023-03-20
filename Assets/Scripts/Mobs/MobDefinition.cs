using System;
using UnityEngine;

namespace Mobs
{
    [Serializable]
    public struct MobDefinition
    {
        public MobType _type;
        public GameObject _prefab;
    }
}