using System.Collections;
using Pathfinding;
using Systems;
using UnityEngine;

namespace Mobs
{
    public class MobAI : MonoBehaviour
    {
        private static readonly int Attacking = Animator.StringToHash("Attacking");
        private static readonly int Running = Animator.StringToHash("Running");
        
        private Coroutine attackCoroutine;
        private float distanceToClosestTargetInVision;
        private Mob mob;
        private Animator mobAnimator;
        private bool stopUpdatingTarget;
        private Transform targetTransform;
        private float timeSinceLastTargetUpdate;

        public Transform TargetTransform
        {
            get => targetTransform;
            set => targetTransform = value;
        }

        public IAstarAI AstarAI { get; private set; }

        private void Start()
        {
            mob = GetComponent<Mob>();
            // _healthSystem = GetComponent<HealthSystem>();
            mobAnimator = GetComponent<Animator>();
            AstarAI = GetComponent<IAstarAI>();

            AstarAI.onSearchPath += Update;
        }

        private void Update()
        {
            timeSinceLastTargetUpdate += Time.deltaTime;

            if (targetTransform != null && AstarAI != null)
                // better to move this outside Update function, we don't need a call every frame 
                AstarAI.destination = targetTransform.position;
        }


        private void OnTriggerEnter(Collider target)
        {
            if (stopUpdatingTarget) return;

            var targetMob = target.GetComponent<Mob>();
            if (targetMob == null) return;

            if (mob.TeamColor == targetMob.TeamColor ||
                targetTransform == targetMob.transform) return;


            targetTransform = targetMob.transform;
            timeSinceLastTargetUpdate = 0f;
            stopUpdatingTarget = true;

            // var enemyHealth = target.GetComponent<IHealthSystem>();
            // attackCoroutine = StartCoroutine(Attack(enemyHealth));
            // mobAnimator.SetBool(Attacking, true);
            Debug.Log($"Target updated: {target.name}");
        }

        private void OnTriggerExit(Collider target)
        {
            var targetMob = target.GetComponent<Mob>();
            if (targetMob == null) return;

            if (target.transform != targetTransform) return;

            targetTransform = null;
            Debug.Log($"Target lost: {target.name}");


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

        private IEnumerator Attack(IHealthSystem enemyHealth)
        {
            // mob.InflictDamage(enemyHealth);
            // enemyHealth.TakeDamage((int) mob._mobData.activeStats[StatType.AttackDamage]._currentValue);
            yield return new WaitForSeconds(1);
        }
    }
}