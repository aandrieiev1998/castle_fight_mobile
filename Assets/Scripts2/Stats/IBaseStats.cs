using System.Collections.Generic;

namespace Scripts2.Stats
{
    public interface IBaseStats
    {
        public Dictionary<StatType, float> Stats { get; }
    }
}