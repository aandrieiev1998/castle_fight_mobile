using System.Collections;
using System.Linq;
using Entities.Buildings;
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

        private Transform targetTransform;
        private Mob mob;
        private Animator mobAnimator;
        private IAstarAI astarAI;
        private Coroutine attackCoroutine;
        private Coroutine deathCoroutine;
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
            deathCoroutine = StartCoroutine(Die());
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
            Debug.Log($"Potential new target: {target.name}");

            var targetEntity = target.GetComponent<GameEntity>();
            if (targetEntity == null) return;

            if (mob.TeamSystem.TeamColor == targetEntity.TeamSystem.TeamColor ||
                targetTransform == targetEntity.transform) return;
            
            Debug.Log($"Acquired new target: {target.name}");
            targetTransform = targetEntity.transform;
            timeSinceLastTargetUpdate = 0f;
            stopUpdatingTarget = true;
            mobAnimator.SetBool(IS_RUNNING, true);
        }

        private void OnTriggerStay(Collider target)
        {
            var targetEntity = target.GetComponent<GameEntity>();
            if (targetEntity == null) return;

            if (target.transform != targetTransform) return;

            /*
             If distance to enemy too far for attack we return.
             If enemy moved out from attack range, but is still in vision, we stop attacking and try move closer.
             */
            
            // RaycastHit hit;
            // if (Physics.Raycast(transform.position, targetTransform.position, out hit, mob.DamageSystem.AttackDistance, LayerMask.GetMask("AI Agent", "AI Obstacle")))
            // {
            //     
            // }
            
            Debug.DrawLine(transform.position, targetTransform.position);
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

                attackCoroutine = StartCoroutine(Attack(targetEntity));
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
            stopUpdatingTarget = false;
            mobAnimator.SetBool(IS_RUNNING, false);
            mobAnimator.SetBool(IS_WALKING, true);
            mobAnimator.SetBool(IS_ATTACKING, false);

            StopCoroutine(attackCoroutine);
            Debug.Log("Target defeated");
        }

        private void OnDestroy()
        {
            if (attackCoroutine != null) StopCoroutine(attackCoroutine);
            if (deathCoroutine != null) StopCoroutine(deathCoroutine);
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

            // Destroy(gameObject);
        }
    }
}