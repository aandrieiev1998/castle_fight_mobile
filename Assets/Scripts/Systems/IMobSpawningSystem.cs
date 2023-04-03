using System;
using Entities.Mobs;
using Match;
using Mobs;
using UnityEngine;

namespace Systems
{
    public interface IMobSpawningSystem
    {
        public Mob MobPrefab { get; }

        public Mob SpawnMob(Vector3 position, TeamColor teamColor);
        public event Action<Mob> MobSpawned;
    }
}