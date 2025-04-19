using UnityEngine;

namespace GameDesign
{
    public class MagicOrb : WeaponBase
    {
        public float checkRadius = 10f;
        public float checkInterval = 0.1f;
        public float speed = 10f;
        public float prepareTime = 2f;

        public LayerMask enemyLayer;        // ֻ������
        public LayerMask obstacleLayer;     // �����ж��ϰ���

        public GameObject hitEffect;        // ��ѡ����Ч��

        private float checkTimer = 0f;
        private Transform target;
        private bool isReady = false;
        private float readyTimer = 0f;

        private void Update()
        {
            // ÿ�� checkInterval ����һ��Ŀ��
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

                // �����Ŀ�ֱ꣬�߷���Ŀ��
                if (target != null)
                {
                    Vector3 direction = (target.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;
                }
                else
                {
                    // Ư���ȴ����ɼ�����ת/����������
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

            // ��������Ƿ��ϰ����赲
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
                // �������
                return;
            }

            if (other.CompareTag("Enemy"))
            {
                // �Ե�������˺�
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

            // ���������κ����壨ǽ������ȣ�������
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
