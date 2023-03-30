using System.Collections;
using Mechanics;
using Pathfinding;
using Stats;
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

        private MobBehaviour mobBehaviour;
        private bool stopUpdatingTarget;
        private float timeSinceLastTargetUpdate;
        private float distanceToClosestTargetInVision;

        private Animator mobAnimator;
        private static readonly int ENEMY_IN_VISION = Animator.StringToHash("EnemyInVision");

        private void Start()
        {
            mobBehaviour = GetComponent<MobBehaviour>();
            _healthSystem = GetComponent<HealthSystem>();
            mobAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastTargetUpdate += Time.deltaTime;
        }

        private void OnTriggerEnter(Collider target)
        {
            if (stopUpdatingTarget) return;

            var targetMobBehaviour = target.GetComponent<MobBehaviour>();
            if (targetMobBehaviour == null) return;

            if (mobBehaviour._mobData._playerTeam != targetMobBehaviour._mobData._playerTeam)
            {
                var destinationTarget = mobDestinationSetter.target;
                if (destinationTarget != targetMobBehaviour.transform)
                {
                    mobDestinationSetter.target = targetMobBehaviour.transform;
                    timeSinceLastTargetUpdate = 0f;
                    stopUpdatingTarget = true;

                    var enemyHealth = target.GetComponent<HealthSystem>();
                    attackCoroutine = StartCoroutine(Attack(enemyHealth));
                    mobAnimator.SetBool(ENEMY_IN_VISION, true);
                    Debug.Log($"Target updated: {destinationTarget}");
                }
            }
        }

        private void OnTriggerExit(Collider target)
        {
            var targetMobBehaviour = target.GetComponent<MobBehaviour>();
            if (targetMobBehaviour == null) return;

            if (targetMobBehaviour._mobData._attackTarget == mobDestinationSetter.target)
            {
                StopCoroutine(attackCoroutine);
                Debug.Log($"Target lost: {mobDestinationSetter.target}");
                mobDestinationSetter.target = null;

                // todo set target to Throne or another enemy in vision
            }
        }

        private IEnumerator Attack(HealthSystem enemyHealth)
        {
            enemyHealth.TakeDamage((int) mobBehaviour._mobData.activeStats[StatType.AttackDamage]._currentValue);
            yield return new WaitForSeconds(1);
        }
    }
}