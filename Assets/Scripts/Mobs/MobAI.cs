using System.Collections;
using System.Linq;
using Buildings;
using Pathfinding;
using Systems;
using UnityEngine;

namespace Mobs
{
    public class MobAI : MonoBehaviour
    {
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int MobHasDied = Animator.StringToHash("MobHasDied");

        private Transform targetTransform;
        private Mob mob;
        private Animator mobAnimator;
        private IAstarAI astarAI;
        private Coroutine attackCoroutine;
        private bool stopUpdatingTarget;
        private float distanceToClosestTargetInVision;
        private float timeSinceLastTargetUpdate;

        private bool wasAttackingInPreviousFrame;

        public Transform TargetTransform
        {
            get => targetTransform;
            set => targetTransform = value;
        }

        private void Awake()
        {
            mob = GetComponent<Mob>();
            mobAnimator = GetComponent<Animator>();

            astarAI = GetComponent<IAstarAI>();
            astarAI.onSearchPath += Update;
            astarAI.maxSpeed = mob.MovementSpeed;

            var capsuleCollider = GetComponent<CapsuleCollider>();
            capsuleCollider.radius = mob.RageDistance;
        }

        private void Start()
        {
            if (targetTransform != null)
                mobAnimator.SetBool(IsWalking, true);
        }

        private void Update()
        {
            timeSinceLastTargetUpdate += Time.deltaTime;

            if (targetTransform != null && astarAI != null)
                // better to move this outside Update function, we don't need a call every frame 
                astarAI.destination = targetTransform.position;

            if (transform.position.y <= -5.0f)
                Destroy(gameObject);
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
            mobAnimator.SetBool(IsRunning, true);
        }

        private void OnTriggerStay(Collider target)
        {
            var targetMob = target.GetComponent<Mob>();
            if (targetMob == null) return;

            if (target.transform != targetTransform) return;

            /*
             If distance to enemy too far for attack we return.
             If enemy moved out from attack range, but is still in vision, we stop attacking and try move closer.
             */
            if (Vector3.Distance(transform.position, targetTransform.position) > mob.AttackDistance)
            {
                if (!wasAttackingInPreviousFrame) return;

                astarAI.isStopped = false;
                wasAttackingInPreviousFrame = false;
                mobAnimator.SetBool(IsAttacking, false);
                mobAnimator.SetBool(IsRunning, true);

                return;
            }

            if (!wasAttackingInPreviousFrame)
            {
                mobAnimator.SetBool(IsAttacking, true);
                mobAnimator.SetBool(IsRunning, false);
            }

            astarAI.isStopped = true;
            wasAttackingInPreviousFrame = true;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            var transformPosition = transform.position;
            Gizmos.DrawWireSphere(transformPosition, mob.AttackDistance);
            Gizmos.DrawWireSphere(transformPosition, mob.RageDistance);
        }

        private void OnTriggerExit(Collider target)
        {
            var targetMob = target.GetComponent<Mob>();
            if (targetMob == null) return;

            if (target.transform != targetTransform) return;

            var enemyCastle = FindObjectsOfType<Castle>().Single(castle => castle.TeamColor != mob.TeamColor);
            targetTransform = enemyCastle.transform;
            astarAI.isStopped = false;
            mobAnimator.SetBool(IsRunning, false);
            mobAnimator.SetBool(IsWalking, true);
            mobAnimator.SetBool(IsAttacking, false);
        }

        private IEnumerator Attack(IHealthSystem enemyHealth)
        {
            // mob.InflictDamage(enemyHealth);
            // enemyHealth.TakeDamage((int) mob._mobData.activeStats[StatType.AttackDamage]._currentValue);
            yield return new WaitForSeconds(1);
        }
    }
}