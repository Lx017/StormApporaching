using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace GameDesign
{
    public class MeleeWeapon : WeaponBase
    {
        //����������(�˺���Դ)
        public PlayerStats owner;


        private HashSet<Collider> damagedEnemies = new HashSet<Collider>();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;


            if (damagedEnemies.Contains(other)) return;

            float damage = CalculateDamage();
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Vector3 knockBackDirection = (hitPoint - transform.position).normalized;

            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null && enemyStats.currentHealth > 0)
            {
                //HitStop.Instance.StopTime(0.1f);
                //CameraShake.Instance.Shake(0.1f);
                Debug.Log("melee");
                if (canKnockBack)
                {
                    enemyStats.TakeDamage(damage, knockBackDirection * knockBackForce, true);
                }
                else
                {
                    enemyStats.TakeDamage(damage, null, true);
                }

                if (ParticleEmitterOH.shared != null)
                {
                    ParticleEmitterOH.shared.EmitParticles(hitPoint, 40);
                    float randn = Random.Range(0f,1f);
                    if (randn < 0.3f)
                    {
				        EnemyStats.DoAOE(hitPoint, 5f, 5f,3f);
                    }
                }

                Debug.Log("currentHealth" + enemyStats.currentHealth);
                damagedEnemies.Add(other);

                StartCoroutine(RemoveFromDamagedEnemiesAfterDelay(other, 0.3f));
            }
        }

        private IEnumerator RemoveFromDamagedEnemiesAfterDelay(Collider enemy, float delay)
        {
            yield return new WaitForSeconds(delay);
            damagedEnemies.Remove(enemy);
        }

        private void OnTriggerExit(Collider other)
        {
            //if (other.CompareTag("Enemy"))
            //{
                //damagedEnemies.Remove(other);
            //}
        }


        public void ClearList()
        {
            //damagedEnemies.Clear();
        }
        public override float CalculateDamage()
        {
            float damage = baseDamage;

            if (owner != null)
            {
                //�����˺�=�����˺�+��ɫ�˺�
                damage += owner.attackPower;
            }

            if (owner && owner.IsCriticalHit())
            {
                //����
                damage *= owner.critMultiplier;
            }

            return damage;
        }
    }
}


