using System.Collections.Generic;
using UnityEngine;

namespace Scripts2.Stats
{
    public abstract class BaseStats : ScriptableObject, IBaseStats, ISerializationCallbackReceiver
    {
        [SerializeField] private List<BaseStat> _stats;
        private readonly Dictionary<StatType, float> statsDictionary = new();
        
        public Dictionary<StatType, float> Stats => statsDictionary;
        
        public void OnBeforeSerialize()
        {
            statsDictionary.Clear();
        }

        public void OnAfterDeserialize()
        {
            statsDictionary.Clear();
            _stats.ForEach(stat => statsDictionary.Add(stat._statType, stat._baseValue));
        }
    }
}