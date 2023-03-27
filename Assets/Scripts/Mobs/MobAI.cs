using Pathfinding;
using UnityEngine;

namespace Mobs
{
    public class MobAI : MonoBehaviour
    {
        private AIDestinationSetter mobDestinationSetter;

        public AIDestinationSetter MobDestinationSetter
        {
            get => mobDestinationSetter;
            set => mobDestinationSetter = value;
        }

        private MobData mobData;
        private bool stopUpdatingTarget;
        private float timeSinceLastTargetUpdate;
        private float distanceToClosestTargetInVision;

        private Animator mobAnimator;
        private static readonly int ENEMY_IN_VISION = Animator.StringToHash("EnemyInVision");

        private void Start()
        {
            mobData = GetComponent<MobData>();
            mobAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastTargetUpdate += Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (stopUpdatingTarget) return;

            var colliderMobData = other.GetComponent<MobData>();
            if (colliderMobData == null) return;

            if (mobData._currentTeam != colliderMobData._currentTeam)
            {
                var target = mobDestinationSetter.target;
                if (target != colliderMobData.transform)
                {
                    mobDestinationSetter.target = colliderMobData.transform;
                    timeSinceLastTargetUpdate = 0f;
                    stopUpdatingTarget = true;

                    mobAnimator.SetBool(ENEMY_IN_VISION, true);
                    Debug.Log($"Target updated: {target}");
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var colliderMobData = other.GetComponent<MobData>();
            if (colliderMobData == null) return;

            if (colliderMobData._target == mobDestinationSetter.target)
            {
                Debug.Log($"Target lost: {mobDestinationSetter.target}");
                mobDestinationSetter.target = null;

                // todo set target to Throne or another enemy in vision
            }
        }
    }
}