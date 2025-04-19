using UnityEngine;

namespace GameDesign
{
    public class Level2Trigger : MonoBehaviour
    {
        public GameObject[] enemyPrefabs;      // ����Ԥ��������
        public Vector3[] spawnPositions;       // �������λ��
        public Transform player;
        public GameObject canvasObject;
        public float lowerHealth = 20f;
        public float upperHealth = 40f;
        private bool hasSpawned = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void OnTriggerEnter(Collider other)
        {
            if (!hasSpawned && other.CompareTag("Player"))
            {
                SpawnRandomEnemy();
                hasSpawned = true;
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void SpawnRandomEnemy()
        {
            foreach (Vector3 position in spawnPositions)
            {
                int enemyIndex = Random.Range(0, enemyPrefabs.Length);
                GameObject enemyInstance = Instantiate(enemyPrefabs[enemyIndex], position, Quaternion.identity);
                EnemyStats stats = enemyInstance.GetComponent<EnemyStats>();
                if (stats != null)
                {
                    stats.currentHealth = Random.Range(lowerHealth, upperHealth); // ������滻�������趨��ֵ
                    stats.maxHealth = stats.currentHealth;
                }

                GolemEnemyAI ai = enemyInstance.GetComponent<GolemEnemyAI>();
                if (ai != null)
                {
                    ai.player = player;
                    ai.uicanvas = canvasObject;
                    ai.disableAI = false;
                }

                IceEnemyAI iai = enemyInstance.GetComponent<IceEnemyAI>();
                if (iai != null)
                {
                    iai.player = player;
                    iai.uicanvas = canvasObject;
                    //iai.disableAI = false;
                }
                FireEnemyAI fai = enemyInstance.GetComponent<FireEnemyAI>();
                if (fai != null)
                {
                    fai.player = player;
                    fai.uicanvas = canvasObject;
                    //fai.disableAI = false;
                }

                BearAI bai = enemyInstance.GetComponent<BearAI>();
                if (bai != null)
                {
                    bai.player = player;
                    bai.uicanvas = canvasObject;
                }
            }
        }
    }
}
