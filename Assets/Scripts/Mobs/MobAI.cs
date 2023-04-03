using System;
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
        private static readonly int MobHasDied = Animator.StringToHash("HasDied");

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
            mobAnimator.SetTrigger(MobHasDied);
            
            Destroy(gameObject, 1.0f);
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
            if (Vector3.Distance(transform.position, targetTransform.position) > mob.DamageSystem.AttackDistance)
            {
                if (!wasAttackingInPreviousFrame) return;

                isAttacking = false;
                astarAI.isStopped = false;
                wasAttackingInPreviousFrame = false;
                mobAnimator.SetBool(IsAttacking, false);
                mobAnimator.SetBool(IsRunning, true);
                
                StopCoroutine(attackCoroutine);

                return;
            }

            if (!wasAttackingInPreviousFrame)
            {
                isAttacking = true;
                astarAI.isStopped = true;
                mobAnimator.SetBool(IsAttacking, true);
                mobAnimator.SetBool(IsRunning, false);

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

            var enemyCastle = FindObjectsOfType<Castle>().Single(castle => castle.TeamColor != mob.TeamSystem.TeamColor);
            targetTransform = enemyCastle.transform;
            astarAI.isStopped = false;
            isAttacking = false;
            mobAnimator.SetBool(IsRunning, false);
            mobAnimator.SetBool(IsWalking, true);
            mobAnimator.SetBool(IsAttacking, false);
            
            StopCoroutine(attackCoroutine);
        }

        // private void OnDisable()
        // {
        //     StopCoroutine(attackCoroutine);
        // }

        private IEnumerator Attack(IHealthSystem enemyHealth)
        {
            while (true)
            {
                mob.DamageSystem.InflictDamage(enemyHealth);
                
                yield return new WaitForSeconds(1);    
            }
            
        }
    }
}