using UnityEngine;
using System.Collections.Generic;

namespace GameDesign
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance { get; private set; }

        private HashSet<EnemyStats> enemies = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Debug.Log("[EnemyManager] Singleton Initialized.");
            }
            else
            {
                Debug.LogError("[EnemyManager] Multiple instances detected!");
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// ע��һ�����ˣ�����ʱ���ã�
        /// </summary>
        public void RegisterEnemy(EnemyStats enemy)
        {
            if (enemy != null)
                enemies.Add(enemy);
        }

        /// <summary>
        /// ע��һ�����ˣ�����������ʱ���ã�
        /// </summary>
        public void UnregisterEnemy(EnemyStats enemy)
        {
            if (enemy != null)
                enemies.Remove(enemy);
        }

        /// <summary>
        /// ��ȡ���� origin ����ĵ���
        /// </summary>
        public EnemyStats GetClosestEnemy(Vector3 origin)
        {
            float minDistance = Mathf.Infinity;
            EnemyStats closest = null;

            foreach (var enemy in enemies)
            {
                if (enemy == null) continue;

                float dist = Vector3.Distance(origin, GetEnemyPos(enemy));
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closest = enemy;
                }
            }

            return closest;
        }

        /// <summary>
        /// �������η�Χ�ڵĵ���
        /// </summary>
        public List<EnemyStats> GetEnemiesInRange(Vector3 origin, float radius)
        {
            List<EnemyStats> result = new();

            foreach (var enemy in enemies)
            {
                if (enemy == null) continue;

                float dist = Vector3.Distance(origin, GetEnemyPos(enemy));
                if (dist <= radius)
                {
                    result.Add(enemy);
                }
            }

            return result;
        }

        /// <summary>
        /// ��ǰ�������Ƿ��е���
        /// </summary>
        public bool HasAliveEnemies()
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                    return true;
            }

            return false;
        }

        public Vector3 GetEnemyPos(EnemyStats enemy)
        {
            Vector3 enemyPos = enemy.GetComponent<Collider>()?.bounds.center ?? enemy.transform.position;
            return enemyPos;
        }

        public Vector3 GetEnemyPos(Transform enemy)
        {
            Vector3 enemyPos = enemy.GetComponent<Collider>()?.bounds.center ?? enemy.transform.position;
            return enemyPos;
        }
    }
}
