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
                Destroy(gameObject); // ��ֹ�ظ�ʵ��
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

                    // �������߼�����
                    if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 10f, LayerMask.GetMask("Default", "Ground")))
                    {
                        // ������ɹ� �� �������ɵ�
                        Vector3 spawnPos = hit.point + Vector3.up * healthSpawnOffset; // ����� 0.5f ����

                        Instantiate(healthPackPrefab, spawnPos, Quaternion.identity);
                    }
                    else
                    {
                        // û��⵽���� �� ʹ��Ĭ��λ��
                        Vector3 fallback = origin;
                        Instantiate(healthPackPrefab, fallback, Quaternion.identity);
                    }
                }
            }

        }


    }
}
