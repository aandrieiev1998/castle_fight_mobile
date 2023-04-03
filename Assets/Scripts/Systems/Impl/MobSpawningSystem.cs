using System;
using System.Linq;
using Buildings;
using Entities.Buildings;
using Entities.Mobs;
using Match;
using Mobs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.Impl
{
    [Serializable]
    public class MobSpawningSystem : IMobSpawningSystem
    {
        [SerializeField] private Mob _spawnedMob;

        public Mob MobPrefab => _spawnedMob;
        
        public Mob SpawnMob(Vector3 position, TeamColor teamColor)
        {
            var mob = Object.Instantiate(_spawnedMob, position, Quaternion.identity);
            mob.TeamSystem.TeamColor = teamColor;

            var enemyCastle = Object.FindObjectsOfType<Castle>()
                .Single(castle => castle.TeamSystem.TeamColor != mob.TeamSystem.TeamColor);

            var mobAI = mob.GetComponent<MobAI>();
            mobAI.TargetTransform = enemyCastle.transform;

            return mob;
        }

        public event Action<Mob> MobSpawned;
    }
}