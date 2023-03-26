using System.Collections.Generic;
using UnityEngine;

namespace Match
{
    public class SpawnpointsContainer : MonoBehaviour
    {
        [SerializeField] private List<Spawnpoint> spawnpoints;

        public List<Spawnpoint> Spawnpoints => spawnpoints;
    }
}