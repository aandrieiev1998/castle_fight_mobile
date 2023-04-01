using System.Collections;
using System.Collections.Generic;
using Match;
using Scripts3.Buildings;
using Scripts3.Mechanics;
using UnityEngine;

namespace Mobs
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _mobPrefabs;
        [SerializeField] private Transform _mobsParent; // todo refactor this for 2 teams
        // [SerializeField] private List<TeamMaterial> _teamMaterials;
        

        private void BuildingAdded(Building building)
        {
            if (building.GetType() == typeof(MobBuilding))
            {
                ((MobBuilding) building).SpawnMobs();
            }
        }

        private IEnumerator StartSpawningMobsForSingleBuilding(TeamColor teamColor, MobType mobType, float interval,
            Vector3 origin)
        {
            while (true)
            {
                //
                yield return new WaitForSeconds(interval);

                // SpawnMob(teamColor, mobType, origin);
            }
        }

        
    }
}