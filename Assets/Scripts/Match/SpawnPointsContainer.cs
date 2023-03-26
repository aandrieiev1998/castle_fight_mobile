using System.Collections.Generic;
using UnityEngine;

namespace Match
{
    public class SpawnPointsContainer : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> _baseBuildingSpawnPoints;
        [SerializeField] private List<SpawnPoint> _mobBuildingsSpawnPoints;

        public List<SpawnPoint> BaseBuildingSpawnPoints => _baseBuildingSpawnPoints;

        public List<SpawnPoint> MobBuildingsSpawnPoints => _mobBuildingsSpawnPoints;
    }
}