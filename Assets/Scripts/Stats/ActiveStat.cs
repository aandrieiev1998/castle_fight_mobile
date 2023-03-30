using System;

namespace Stats
{
    [Serializable]
    public class ActiveStat
    {
        public StatType _statType;
        public float _currentValue;
        public float _maxValue;

        public ActiveStat(StatType statType, float currentValue, float maxValue)
        {
            _statType = statType;
            _currentValue = currentValue;
            _maxValue = maxValue;
        }
    }
}