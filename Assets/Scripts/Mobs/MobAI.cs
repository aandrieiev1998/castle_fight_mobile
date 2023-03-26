using System;
using Mechanics;
using UnityEngine;

namespace Mobs
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("AI Obstacle"))
            {
                Debug.Log(other.name);
                // other.gameObject.GetComponent<IReceiveDamage>().FinalDamage(MobData.)
                // int targetHp = other.gameObject.GetComponent<MobData>()._currentHp;
                // int targetHp = other.gameObject.GetComponent<MobData>()._curre;
            }
        }
    }
}