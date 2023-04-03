using System.Collections;
using System.Linq;
using Buildings;
using Entities.Buildings;
using Pathfinding;
using Systems;
using UnityEngine;

namespace Entities.Mobs
{
    public class MobAI : MonoBehaviour
    {
        private static readonly int IS_ATTACKING = Animator.StringToHash("IsAttacking");
        private static readonly int IS_RUNNING = Animator.StringToHash("IsRunning");
        private static readonly int IS_WALKING = Animator.StringToHash("IsWalking");
        private static readonly int MOB_HAS_DIED = Animator.StringToHash("HasDied");

        private Transform targetTransform;
        private Mob mob;
        private Animator mobAnimator;
        private IAstarAI astarAI;
        private Coroutine attackCoroutine;
        private bool stopUpdatingTarget;
        private float distanceToClosestTargetInVision;
        private float timeSinceLastTargetUpdate;

        private bool wasAttackingInPreviousFrame;
        private bool isAttacking;

        public Transform TargetTransform
        {
            get => targetTransform;
            set => targetTransform = value;
        }

        private void Awake()
        {
            mob = GetComponent<Mob>();
            mob.HealthSystem.Death += OnMobDeath;

            mobAnimator = GetComponent<Animator>();

            astarAI = GetComponent<IAstarAI>();
            astarAI.onSearchPath += Update;
            astarAI.maxSpeed = mob.MovementSystem.MovementSpeed;

            var capsuleCollider = GetComponent<CapsuleCollider>();
            capsuleCollider.radius = mob.RageDistance;
        }

        private void OnMobDeath()
        {
            mobAnimator.SetTrigger(MOB_HAS_DIED);

            Destroy(gameObject, 2.0f);
        }

        private void Start()
        {
            if (targetTransform != null)
                mobAnimator.SetBool(IS_WALKING, true);
        }

        private void Update()
        {
            timeSinceLastTargetUpdate += Time.deltaTime;

            if (targetTransform != null && astarAI != null)
                // better to move this outside Update function, we don't need a call every frame 
                astarAI.destination = targetTransform.position;

            if (isAttacking)
            {
                transform.LookAt(targetTransform);
            }

            if (transform.position.y <= -5.0f)
                Destroy(gameObject);
        }


        private void OnTriggerEnter(Collider target)
        {
            if (stopUpdatingTarget) return;

            var targetMob = target.GetComponent<Mob>();
            if (targetMob == null) return;

            if (mob.TeamSystem.TeamColor == targetMob.TeamSystem.TeamColor ||
                targetTransform == targetMob.transform) return;

            targetTransform = targetMob.transform;
            timeSinceLastTargetUpdate = 0f;
            stopUpdatingTarget = true;
            mobAnimator.SetBool(IS_RUNNING, true);
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
            if (Vector3.Distance(transform.position, targetTransform.position) > mob.DamageSystem.AttackDistance)
            {
                if (!wasAttackingInPreviousFrame) return;

                isAttacking = false;
                astarAI.isStopped = false;
                wasAttackingInPreviousFrame = false;
                mobAnimator.SetBool(IS_ATTACKING, false);
                mobAnimator.SetBool(IS_RUNNING, true);

                StopCoroutine(attackCoroutine);

                return;
            }

            if (!wasAttackingInPreviousFrame)
            {
                isAttacking = true;
                astarAI.isStopped = true;
                mobAnimator.SetBool(IS_ATTACKING, true);
                mobAnimator.SetBool(IS_RUNNING, false);

                attackCoroutine = StartCoroutine(Attack(targetMob.HealthSystem));
            }

            wasAttackingInPreviousFrame = true;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            var transformPosition = transform.position;
            Gizmos.DrawWireSphere(transformPosition, mob.DamageSystem.AttackDistance);
            Gizmos.DrawWireSphere(transformPosition, mob.RageDistance);
        }

        private void OnTriggerExit(Collider target)
        {
            var targetMob = target.GetComponent<Mob>();
            if (targetMob == null) return;

            if (target.transform != targetTransform) return;

            var enemyCastle = FindObjectsOfType<Castle>()
                .Single(castle => castle.TeamSystem.TeamColor != mob.TeamSystem.TeamColor);
            targetTransform = enemyCastle.transform;
            astarAI.isStopped = false;
            isAttacking = false;
            mobAnimator.SetBool(IS_RUNNING, false);
            mobAnimator.SetBool(IS_WALKING, true);
            mobAnimator.SetBool(IS_ATTACKING, false);

            StopCoroutine(attackCoroutine);
        }

        private void OnDisable()
        {
            if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        }

        private IEnumerator Attack(IHealthSystem enemyHealth)
        {
            while (true)
            {
                yield return new WaitForSeconds(mob.DamageSystem.AttackSpeed);
                
                mob.DamageSystem.InflictDamage(enemyHealth);
            }
        }
    }
}