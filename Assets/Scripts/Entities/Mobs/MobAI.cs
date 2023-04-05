using System;
using System.Collections;
using System.Linq;
using Entities.Buildings;
using Mechanics;
using Pathfinding;
using UnityEngine;

namespace Entities.Mobs
{
    public class MobAI : MonoBehaviour
    {
        private static readonly int IS_ATTACKING = Animator.StringToHash("IsAttacking");
        private static readonly int IS_RUNNING = Animator.StringToHash("IsRunning");
        private static readonly int IS_WALKING = Animator.StringToHash("IsWalking");
        private static readonly int MOB_HAS_DIED = Animator.StringToHash("HasDied");
        private IAstarAI astarAI;
        private Coroutine attackCoroutine;
        private AttackTrigger attackTrigger;
        private Coroutine deathCoroutine;
        private float distanceToClosestTargetInVision;
        private bool isAttacking;
        private bool isInAttackRange;
        private Mob mob;
        private Animator mobAnimator;
        private bool stopUpdatingTarget;

        private float timeSinceLastTargetUpdate;

        private bool wasAttackingInPreviousFrame;

        public Transform TargetTransform { get; set; }

        private void Awake()
        {
            mob = GetComponent<Mob>();
            mob.HealthSystem.Death += OnMobDeath;

            mobAnimator = GetComponent<Animator>();

            astarAI = GetComponent<IAstarAI>();
            astarAI.onSearchPath += Update;
            astarAI.maxSpeed = mob.MovementSystem.MovementSpeed;

            var rageTrigger = GetComponent<CapsuleCollider>();
            rageTrigger.radius = mob.RageDistance;

            attackTrigger = GetComponentInChildren<AttackTrigger>();
            attackTrigger.CapsuleCollider.radius = mob.DamageSystem.AttackDistance;

            attackTrigger.OnEnter += AttackTriggerOnEnter;
            attackTrigger.OnExit += AttackTriggerOnExit;
        }

        private void Start()
        {
            if (TargetTransform != null)
                mobAnimator.SetBool(IS_WALKING, true);
        }

        private void Update()
        {
            timeSinceLastTargetUpdate += Time.deltaTime;

            if (TargetTransform != null && astarAI != null)
                // better to move this outside Update function, we don't need a call every frame 
                astarAI.destination = TargetTransform.position;

            if (isAttacking) transform.LookAt(TargetTransform);

            if (transform.position.y <= -5.0f)
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (attackCoroutine != null) StopCoroutine(attackCoroutine);
            if (deathCoroutine != null) StopCoroutine(deathCoroutine);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            // var transformPosition = transform.position;
            // Gizmos.DrawWireSphere(transformPosition, mob.DamageSystem.AttackDistance);
            // Gizmos.DrawWireSphere(transformPosition, mob.RageDistance);
        }

        private void OnTriggerEnter(Collider target)
        {
            // Debug.Log($"Rage trigger enter: {target.name}");
            if (stopUpdatingTarget) return;

            var targetEntity = target.GetComponent<GameEntity>();
            if (targetEntity == null) return;

            if (mob.TeamSystem.TeamColor == targetEntity.TeamSystem.TeamColor ||
                TargetTransform == targetEntity.transform) return;

            // Debug.Log($"Acquired new target: {target.name}");
            TargetTransform = targetEntity.transform;
            timeSinceLastTargetUpdate = 0f;
            stopUpdatingTarget = true;
            mobAnimator.SetBool(IS_RUNNING, true);
        }

        private void OnTriggerExit(Collider target)
        {
            var targetMob = target.GetComponent<Mob>();
            if (targetMob == null) return;

            if (target.transform != TargetTransform) return;

            var enemyCastle = FindObjectsOfType<Castle>()
                .Single(castle => castle.TeamSystem.TeamColor != mob.TeamSystem.TeamColor);
            TargetTransform = enemyCastle.transform;
            astarAI.isStopped = false;
            isAttacking = false;
            stopUpdatingTarget = false;
            mobAnimator.SetBool(IS_RUNNING, false);
            mobAnimator.SetBool(IS_WALKING, true);
            mobAnimator.SetBool(IS_ATTACKING, false);

            StopCoroutine(attackCoroutine);
        }

        private void OnTriggerStay(Collider target)
        {
            var targetEntity = target.GetComponent<GameEntity>();
            if (targetEntity == null) return;

            if (target.transform != TargetTransform) return;

            // Debug.Log($"isInAttackRange = {isInAttackRange}");
            // if (Vector3.Distance(transform.position, TargetTransform.position) > mob.DamageSystem.AttackDistance)
            if (!isInAttackRange)
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

                attackCoroutine = StartCoroutine(Attack(targetEntity));
            }

            wasAttackingInPreviousFrame = true;
        }

        private void AttackTriggerOnExit(Collider target)
        {
            Debug.Log($"Attack trigger exit: {target.name}");
            isInAttackRange = false;
        }

        private void AttackTriggerOnEnter(Collider target)
        {
            Debug.Log($"Attack trigger enter: {target.name}");
            isInAttackRange = true;
        }

        private void OnMobDeath()
        {
            deathCoroutine = StartCoroutine(Die());
        }


        private IEnumerator Attack(GameEntity targetEntity)
        {
            while (!targetEntity.HealthSystem.IsDead)
            {
                yield return new WaitForSeconds(mob.DamageSystem.AttackSpeed);

                // check if "I" am not dead myself before attack
                // Debug.Log($"I AM {mob.TeamSystem.TeamColor} I HAVE {mob.HealthSystem.HealthAmount}");

                if (!mob.HealthSystem.IsDead)
                    mob.DamageSystem.InflictDamage(targetEntity.HealthSystem);
            }

            // Debug.Log($"I AM WINNER {mob.TeamSystem.TeamColor}");
            isAttacking = false;
            // mobAnimator.SetBool(IS_ATTACKING, false);
        }

        private IEnumerator Die()
        {
            mobAnimator.SetTrigger(MOB_HAS_DIED);

            yield return new WaitForSeconds(1.3f);

            transform.Translate(new Vector3(1000f, 1000f, 1000f));

            yield return new WaitForSeconds(0.1f);

            Destroy(gameObject);
        }
    }
}