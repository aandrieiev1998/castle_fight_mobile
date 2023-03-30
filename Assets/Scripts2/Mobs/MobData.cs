using System;
using System.Collections.Generic;
using Scripts2.Stats;

namespace Scripts2.Mobs
{
    [Serializable]
    public class MobData
    {
        public List<ActiveStat> _activeStats = new();
    }
}