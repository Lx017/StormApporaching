using UnityEngine;
using UnityEngine.Events;

namespace GameDesign
{
    public class CombatRewardManager : MonoBehaviour
    {
        public static CombatRewardManager Instance { get; private set; }
        public float healthSpawnOffset = 0.5f;
        public GameObject healthPackPrefab;
        private UnityAction<EnemyStats> enemyDiedListener;
        public float healthDropChance = 0.1f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // 防止重复实例
                return;
            }
            Instance = this;
            enemyDiedListener = new UnityAction<EnemyStats>(OnEnemyDied);
        }
        private void OnEnable()
        {
            EventManager.StartListening<EnemyDiedEvent, EnemyStats>(enemyDiedListener);
        }

        private void OnDisable()
        {
            EventManager.StopListening<EnemyDiedEvent, EnemyStats>(enemyDiedListener);
        }

        private void OnEnemyDied(EnemyStats enemy)
        {
            GiveExp(enemy);
            TryDropLoot(enemy);
        }

        private void GiveExp(EnemyStats enemy)
        {
            EventManager.TriggerEvent<ExperienceEvent, float>(enemy.experienceReward);
        }

        private void TryDropLoot(EnemyStats enemy)
        {
            if (healthPackPrefab != null)
            {
                if (Random.value < healthDropChance)
                {
                    Vector3 origin = enemy.GetComponent<Collider>()?.bounds.center ?? enemy.transform.position;

                    // 向下射线检测地面
                    if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 10f, LayerMask.GetMask("Default", "Ground")))
                    {
                        // 地面检测成功 → 调整生成点
                        Vector3 spawnPos = hit.point + Vector3.up * healthSpawnOffset; // 离地面 0.5f 悬浮

                        Instantiate(healthPackPrefab, spawnPos, Quaternion.identity);
                    }
                    else
                    {
                        // 没检测到地面 → 使用默认位置
                        Vector3 fallback = origin;
                        Instantiate(healthPackPrefab, fallback, Quaternion.identity);
                    }
                }
            }

        }


    }
}
