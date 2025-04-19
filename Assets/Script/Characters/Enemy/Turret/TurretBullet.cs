using UnityEngine;

namespace GameDesign
{
    public class TurretBullet : MonoBehaviour
    {
        public float speed = 20f;
        public float lifeTime = 5f;
        public float damage = 10f;
        public LayerMask hitMask;

        private Vector3 direction;

        void Start()
        {
            direction = transform.forward;
            Destroy(gameObject, lifeTime);
        }

        void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        void OnTriggerEnter(Collider other)
        {
            // if (((1 << other.gameObject.layer) & hitMask) == 0)
            //     return;

            // 如果是玩家
            if (other.CompareTag("Player"))
            {
                var playerStats = other.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(damage);
                }
            }
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            EnemyStats.DoAOE(hitPoint, 5f, 5f, 3f);//just for visual effect
            // 命中任何目标后销毁
            Destroy(gameObject);
        }
    }
}
