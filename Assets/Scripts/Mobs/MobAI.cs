using System.Collections;
using System.Linq;
using Buildings;
using Mechanics;
using Pathfinding;
using Stats;
using UnityEngine;

namespace Mobs
{
    public class MobAI : MonoBehaviour
    {
        public AIDestinationSetter _mobDestinationSetter;
        public BuildingContainer _buildingContainer;

        private Coroutine attackCoroutine;
        private HealthSystem _healthSystem;
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
                var destinationTarget = _mobDestinationSetter.target;
                if (destinationTarget != targetMobBehaviour.transform)
                {
                    _mobDestinationSetter.target = targetMobBehaviour.transform;
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

            if (_mobDestinationSetter.target == target.transform)
            {
                StopCoroutine(attackCoroutine);
                Debug.Log($"Target lost: {_mobDestinationSetter.target}");
                
                var enemyThrone = _buildingContainer._buildings.Single(bb =>
                    bb._buildingData._playerTeam != mobBehaviour._mobData._playerTeam &&
                    bb._buildingData._buildingType == BuildingType.Throne);

                _mobDestinationSetter.target = enemyThrone.transform;
            }
        }

        private IEnumerator Attack(HealthSystem enemyHealth)
        {
            enemyHealth.TakeDamage((int) mobBehaviour._mobData.activeStats[StatType.AttackDamage]._currentValue);
            yield return new WaitForSeconds(1);
        }
    }
}