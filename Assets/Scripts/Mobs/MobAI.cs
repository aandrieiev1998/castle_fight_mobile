using System.Collections;
using System.Linq;
using Buildings;
using Match;
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
        private IAstarAI astarAI;

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
            astarAI.isStopped = true;
        }

        private void OnTriggerStay(Collider target)
        {
            var targetMob = target.GetComponent<Mob>();
            if (targetMob == null) return;

            if (target.transform != targetTransform) return;

            if (Vector3.Distance(transform.position, targetTransform.position) > mob.AttackDistance) return;
            
            Debug.Log($"{mob.name } is attacking");
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
        }

        private IEnumerator Attack(IHealthSystem enemyHealth)
        {
            // mob.InflictDamage(enemyHealth);
            // enemyHealth.TakeDamage((int) mob._mobData.activeStats[StatType.AttackDamage]._currentValue);
            yield return new WaitForSeconds(1);
        }
    }
}