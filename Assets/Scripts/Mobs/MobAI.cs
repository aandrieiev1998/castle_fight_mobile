using System.Collections;
using Mechanics;
using Pathfinding;
using UnityEngine;

namespace Mobs
{
    public class MobAI : MonoBehaviour
    {
        private AIDestinationSetter mobDestinationSetter;
        private Coroutine attackCoroutine;
        private HealthSystem _healthSystem;

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
        private static readonly int Attacking = Animator.StringToHash("Attacking");
        private static readonly int Running = Animator.StringToHash("Running");

        private void Start()
        {
            mobData = GetComponent<MobData>();
            _healthSystem = GetComponent<HealthSystem>();
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

                    var enemyHealth = other.GetComponent<HealthSystem>();
                    attackCoroutine = StartCoroutine(Attack(enemyHealth));
                    mobAnimator.SetBool(Attacking, true);
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
                StopCoroutine(attackCoroutine);
                Debug.Log($"Target lost: {mobDestinationSetter.target}");
                mobDestinationSetter.target = null;
            }
        }

        private IEnumerator Attack(HealthSystem enemyHealth)
        {
            enemyHealth.TakeDamage(mobData._currentDamage);
            yield return new WaitForSeconds(1);
        }
    }
}