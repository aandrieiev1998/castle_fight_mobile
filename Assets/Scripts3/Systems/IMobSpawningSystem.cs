using Scripts3.Mechanics;

namespace Scripts3.Systems
{
    public interface IMobSpawningSystem
    {
        public MobType MobType { get; set; }
        public int MobAmount { get; set; }
    }
}