using UnityEngine;

namespace GameDesign
{
    public class EnemyPatrol : MonoBehaviour
    {
        public float moveSpeed = 1f;
        public float moveDistance = 3f;

        private Vector3 startPosition;
        private int direction = 1;

        public GameObject healthBarPrefab;
        public GameObject uicanvas;
        private GameObject bar;

        private PassiveEnemyStats stats;

        void Start()
        {
            startPosition = transform.position;
            stats = GetComponent<PassiveEnemyStats>(); // ✅ 不是继承，而是组合

            // 添加血条
            if (healthBarPrefab != null && uicanvas != null && stats != null)
            {
                bar = Instantiate(healthBarPrefab, uicanvas.transform);
                HealthBarUI barUI = bar.GetComponent<HealthBarUI>();
                barUI.target = stats; // ✅ 指向真正的血量控制器
            }
        }

        void Update()
        {
            transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - startPosition.x) > moveDistance)
            {
                direction *= -1;
            }
        }

        public void OnDeath()
        {
            if (bar != null) Destroy(bar);
        }
    }
}
