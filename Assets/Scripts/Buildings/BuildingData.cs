using System;

namespace Buildings
{
    [Serializable]
    public struct BuildingData
    {
        public int Id { get; set; }
        public int CurrentHp { get; set; }
    }
}