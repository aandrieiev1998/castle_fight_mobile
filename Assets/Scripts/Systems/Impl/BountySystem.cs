using System;
using UnityEngine;

namespace Systems.Impl
{
    [Serializable]
    public class BountySystem : IBountySystem
    {
        [SerializeField] private int _goldForKill;
        [SerializeField] private int _experienceForKill;

        public int GoldForKill
        {
            get => _goldForKill;
            set => _goldForKill = value;
        }

        public int ExperienceForKill
        {
            get => _experienceForKill;
            set => _experienceForKill = value;
        }
    }
}