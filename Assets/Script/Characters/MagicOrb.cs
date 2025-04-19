using UnityEngine;

namespace GameDesign
{
    public class MagicOrb : WeaponBase
    {
        public float checkRadius = 10f;
        public float checkInterval = 0.1f;
        public float speed = 10f;
        public float prepareTime = 2f;

        public LayerMask enemyLayer;        // 只检测敌人
        public LayerMask obstacleLayer;     // 射线判断障碍物

        public GameObject hitEffect;        // 可选击中效果

        private float checkTimer = 0f;
        private Transform target;
        private bool isReady = false;
        private float readyTimer = 0f;

        private void Update()
        {
            // 每隔 checkInterval 秒检测一次目标
            checkTimer += Time.deltaTime;
            if (isReady)
            {
                if (checkTimer >= checkInterval && target == null)
                {
                    checkTimer = 0f;
                    FindTarget();
                    if (target != null)
                    {
                        transform.parent = null;
                    }
                }

                // 如果有目标，直线飞向目标
                if (target != null)
                {
                    Vector3 direction = (target.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;
                }
                else
                {
                    // 漂浮等待（可加入旋转/悬浮动画）
                    transform.Rotate(Vector3.up * 60f * Time.deltaTime);
                }
            }
            else
            {
                readyTimer += Time.deltaTime;
                if (readyTimer >= prepareTime)
                {
                    isReady = true;
                }
            }
        }

        private void FindTarget()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, checkRadius, enemyLayer);
            float closestDist = Mathf.Infinity;
            Transform closest = null;

            foreach (Collider col in hits)
            {
                Transform aimPoint = col.transform;

                if (col.TryGetComponent<EnemyTarget>(out var enemyTarget) && enemyTarget.aimPoint != null)
                {
                    aimPoint = enemyTarget.aimPoint;
                }

                float dist = Vector3.Distance(transform.position, aimPoint.position);

                if (dist < closestDist && HasClearPath(aimPoint))
                {
                    closestDist = dist;
                    closest = aimPoint;
                }
            }

            target = closest;
        }

        private bool HasClearPath(Transform potentialTarget)
        {
            Vector3 dir = potentialTarget.position - transform.position;
            float distance = dir.magnitude;

            // 检测射线是否被障碍物阻挡
            if (Physics.Raycast(transform.position, dir.normalized, distance, obstacleLayer))
            {
                return false;
            }

            return true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // 忽略玩家
                return;
            }

            if (other.CompareTag("Enemy"))
            {
                // 对敌人造成伤害
                if (other.TryGetComponent(out EnemyStats enemy))
                {
                    float damage = CalculateDamage();
                    enemy.TakeDamage(damage);
                }

                if (hitEffect != null)
                {
                    Instantiate(hitEffect, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
                return;
            }

            // 碰到其他任何物体（墙、地面等）则销毁
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0.5f, 0f, 0.25f);
            Gizmos.DrawSphere(transform.position, checkRadius);
        }

        public override float CalculateDamage()
        {
            float damage = base.CalculateDamage();

            return damage;
        }
    }
}
