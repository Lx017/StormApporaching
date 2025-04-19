using UnityEngine;
using UnityEngine.Events;

namespace GameDesign
{
    public class AegisCore : MonoBehaviour
    {
        public float maxShieldBonus = 30f;
        public float damageToShieldRatio = 0.2f;
        private UnityAction<float> enemyDamagedListener;
        private PlayerStats playerStats;

        private void Awake()
        {
            enemyDamagedListener = new UnityAction<float>(GainShieldFromDamage);
        }
        void Start()
        {
            playerStats = GetComponentInParent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.maxShield += maxShieldBonus;
            }
        }

        private void OnEnable()
        {
            EventManager.StartListening<EnemyDamagedEvent, float>(enemyDamagedListener);
        }

        private void OnDisable()
        {
            EventManager.StopListening<EnemyDamagedEvent, float>(enemyDamagedListener);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void GainShieldFromDamage(float damage)
        {
            float shieldGain = damage * damageToShieldRatio;
            playerStats.AddShield(shieldGain);
        }


    }
}
