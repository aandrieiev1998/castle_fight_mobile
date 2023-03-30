using System.Collections.Generic;

namespace Stats
{
    public interface IBaseStats
    {
        public Dictionary<StatType, float> Stats { get; }
    }
}