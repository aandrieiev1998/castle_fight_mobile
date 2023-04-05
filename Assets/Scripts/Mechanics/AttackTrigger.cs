using System;
using Entities;
using Entities.Mobs;
using Match;
using UnityEngine;

namespace Mechanics
{
    public class AttackTrigger : MonoBehaviour
    {
        private const string TriggerTag = "AttackTrigger";

        [SerializeField] private CapsuleCollider _capsuleCollider;

        public CapsuleCollider CapsuleCollider => _capsuleCollider;

        public TeamColor TeamColor { get; set; }

        private void Start()
        {
            TeamColor = GetComponentInParent<Mob>().TeamSystem.TeamColor;
            Debug.Log($"TeamColor = {TeamColor}");
        }

        private void OnTriggerEnter(Collider target)
        {
            if (FilterTarget(target))
                OnEnter?.Invoke(target);
        }

        private void OnTriggerExit(Collider target)
        {
            if (FilterTarget(target))
                OnExit?.Invoke(target);
        }

        private void OnTriggerStay(Collider target)
        {
            if (FilterTarget(target))
                OnStay?.Invoke(target);
        }

        private bool FilterTarget(Collider target)
        {
            if (!target.CompareTag(TriggerTag)) return false;

            // var targetEntity = target.GetComponentInParent<GameEntity>();
            // if (targetEntity == null) return false;
            //
            // if (TeamColor == targetEntity.TeamSystem.TeamColor) return false;

            return true;
        }

        public event Action<Collider> OnEnter;
        public event Action<Collider> OnStay;
        public event Action<Collider> OnExit;
    }
}