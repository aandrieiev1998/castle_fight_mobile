using System;
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
            }
        }
    }
}