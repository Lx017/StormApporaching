using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameDesign
{
    public class InfernalCore : WeaponBase
    {
        public float radius = 5f;
        public float tickInterval = 0.2f;

        private float timer = 0f;

        private HashSet<Collider> damagedEnemies = new HashSet<Collider>();

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= tickInterval)
            {
                timer = 0f;
                DamageEnemiesInRadius();
            }
        }

        private void DamageEnemiesInRadius()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));

            foreach (var enemyCollider in enemies)
            {
                if (!enemyCollider.CompareTag("Enemy")) continue;

                EnemyStats enemyStats = enemyCollider.GetComponent<EnemyStats>();
                if (enemyStats == null || enemyStats.currentHealth <= 0) continue;

                float damage = CalculateDamage();
                Vector3 hitPoint = enemyCollider.ClosestPoint(transform.position);

                enemyStats.TakeDamage(damage);

                if (ParticleEmitterOH.shared != null)
                {
                    ParticleEmitterOH.shared.EmitParticles(hitPoint, 20);
                }
				EnemyStats.DoAOE(hitPoint, 5f, 5f,3f);


            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
