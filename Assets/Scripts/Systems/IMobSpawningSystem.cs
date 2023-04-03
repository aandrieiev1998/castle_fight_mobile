using System;
using Mobs;

namespace Systems
{
    public interface IMobSpawningSystem
    {
        public event Action<Mob> MobSpawned;

        public void SpawnMobs();
    }
}