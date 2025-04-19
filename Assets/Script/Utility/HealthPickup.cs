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
            // ������ʱ�������Ƿ�ӵ�л�Ѫ��ǿ����
            var player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                var relicManager = player.GetComponent<RelicManager>();
                if (relicManager != null && relicManager.HasRelic("Vital Bloom"))
                {
                    healAmount *= 2.0f; // ���������� 50% ��Ѫ��
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // ��ȡ��ҵ�Ѫ�����
                if (other.TryGetComponent<PlayerStats>(out var playerStats))
                {
                    playerStats.AddHealth(healAmount);

                    // ��ѡ��Ч
                    if (pickupEffect != null)
                        Instantiate(pickupEffect, transform.position, Quaternion.identity);

                    // ��ѡ��Ч
                    if (pickupSound != null)
                        AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                    // ����Ѫ��
                    Destroy(gameObject);
                }
            }
        }
    }

}
