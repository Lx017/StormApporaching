using UnityEngine;

namespace GameDesign
{
    public class HealthPickup : MonoBehaviour
    {
        public float healAmount = 20f;

        public GameObject pickupEffect;
        public AudioClip pickupSound;

        private void Start()
        {
            // 在生成时检测玩家是否拥有回血增强神器
            var player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                var relicManager = player.GetComponent<RelicManager>();
                if (relicManager != null && relicManager.HasRelic("Vital Bloom"))
                {
                    healAmount *= 2.0f; // 举例：提升 50% 回血量
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // 获取玩家的血量组件
                if (other.TryGetComponent<PlayerStats>(out var playerStats))
                {
                    playerStats.AddHealth(healAmount);

                    // 可选特效
                    if (pickupEffect != null)
                        Instantiate(pickupEffect, transform.position, Quaternion.identity);

                    // 可选音效
                    if (pickupSound != null)
                        AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                    // 销毁血包
                    Destroy(gameObject);
                }
            }
        }
    }

}
