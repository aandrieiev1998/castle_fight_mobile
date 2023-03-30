using System.Collections.Generic;
using Scripts2.Stats;

namespace Scripts2.Data
{
    public interface IBaseData
    {
        public Dictionary<StatType, BaseData> Data { get; }
    }
}