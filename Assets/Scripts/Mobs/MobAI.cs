using Pathfinding;
using Scripts3.Mobs;
using UnityEngine;

namespace Mobs
{
    public class MobAI : MonoBehaviour
    {
        public AIDestinationSetter _mobDestinationSetter;

        private Coroutine attackCoroutine;
        // private HealthSystem _healthSystem;
        private Mob mob;
        private bool stopUpdatingTarget;
        private float timeSinceLastTargetUpdate;
        private float distanceToClosestTargetInVision;

        private Animator mobAnimator;
        private static readonly int Attacking = Animator.StringToHash("Attacking");
        private static readonly int Running = Animator.StringToHash("Running");

        private void Start()
        {
            mob = GetComponent<Mob>();
           // _healthSystem = GetComponent<HealthSystem>();
            mobAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastTargetUpdate += Time.deltaTime;
        }

        private void OnTriggerEnter(Collider target)
        {
            if (stopUpdatingTarget) return;

            var targetMob = target.GetComponent<Mob>();
            if (targetMob == null) return;

            // if (mob._mobData._teamColor != targetMob._mobData._teamColor)
            // {
            //     var destinationTarget = _mobDestinationSetter.target;
            //     if (destinationTarget != targetMob.transform)
            //     {
            //         _mobDestinationSetter.target = targetMob.transform;
            //         timeSinceLastTargetUpdate = 0f;
            //         stopUpdatingTarget = true;
            //
            //         var enemyHealth = target.GetComponent<HealthSystem>();
            //         attackCoroutine = StartCoroutine(Attack(enemyHealth));
            //         mobAnimator.SetBool(Attacking, true);
            //         Debug.Log($"Target updated: {destinationTarget}");
            //     }
            // }
        }

        private void OnTriggerExit(Collider target)
        {
            var targetMob = target.GetComponent<Mob>();
            if (targetMob == null) return;

            // if (_mobDestinationSetter.target == target.transform)
            // {
            //     StopCoroutine(attackCoroutine);
            //     Debug.Log($"Target lost: {_mobDestinationSetter.target}");
            //     
            //     var enemyThrone = _buildingContainer._buildings.Single(bb =>
            //         bb._buildingData._teamColor != mob._mobData._teamColor &&
            //         bb._buildingData._buildingType == BuildingType.Throne);
            //
            //     _mobDestinationSetter.target = enemyThrone.transform;
            //     mobAnimator.SetBool(Running, true);
            // }
        }

        // private IEnumerator Attack(HealthSystem enemyHealth)
        // {
        //     enemyHealth.TakeDamage((int) mob._mobData.activeStats[StatType.AttackDamage]._currentValue);
        //     yield return new WaitForSeconds(1);
        // }
    }
}