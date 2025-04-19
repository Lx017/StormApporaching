using UnityEngine;
using System.Collections.Generic;

namespace GameDesign
{
    public class EnemyDamageArea : WeaponBase
    {
        public int areaID; // �˺������Ψһ ID
        public EnemyStats owner;
        private CapsuleCollider damageCollider;

        private bool hasTakenDamageThisCollision = false;

        private void Awake()
        {
            damageCollider = GetComponent<CapsuleCollider>();
            if (damageCollider != null)
            {
                damageCollider.enabled = false; // Ĭ�Ͻ��� Collider
            }

            // ���Լ�ע�ᵽȫ�ֹ���
            if (!AttackColliderBehavior.AllDamageAreas.ContainsKey(areaID))
            {
                AttackColliderBehavior.AllDamageAreas[areaID] = new List<EnemyDamageArea>();
            }
            AttackColliderBehavior.AllDamageAreas[areaID].Add(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (hasTakenDamageThisCollision || !damageCollider.enabled) return;

            if (other.CompareTag("Player"))
            {
                if (!owner.onAtk)
                {
                    return;
                }

                float damage = CalculateDamage();

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 knockBackDirection = (hitPoint - transform.position).normalized;

                PlayerStats playerStats = other.GetComponent<PlayerStats>();
                if (playerStats != null && playerStats.currentHealth > 0)
                {
                    if (canKnockBack) // ����
                    {
                        playerStats.TakeDamage(damage, knockBackDirection * knockBackForce);
                    }
                    else
                    {
                        playerStats.TakeDamage(damage);
                    }
                }

                hasTakenDamageThisCollision = true;
                DisableCollider(); // ���һ���˺��󣬽�����ײ��
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                hasTakenDamageThisCollision = false;
            }
        }

        public override float CalculateDamage()
        {
            float damage = baseDamage;

            if (owner != null) 
            {
                damage += owner.attackPower;
            }

            if (owner != null && owner.IsCriticalHit())
            {
                damage *= owner.critMultiplier;
            }

            return damage;
        }

        public void EnableCollider()
        {
            if (damageCollider != null)
            {
                damageCollider.enabled = true;
                hasTakenDamageThisCollision = false; // �����˺����
            }
        }

        public void DisableCollider()
        {
            if (damageCollider != null)
            {
                damageCollider.enabled = false;
            }
        }
    }
}
