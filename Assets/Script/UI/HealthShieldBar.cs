using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameDesign
{
    public class HealthShieldBar : MonoBehaviour
    {
        public Image healthFill;
        public Image shieldFill;
        public TextMeshProUGUI healthText;

        public PlayerStats playerStats;

        private void Start()
        {
            if (playerStats == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                    playerStats = player.GetComponent<PlayerStats>();
            }
        }

        void Update()
        {
            if (playerStats == null) return;

            float healthPercent = playerStats.currentHealth / playerStats.maxHealth;
            float shieldPercent = playerStats.currentShield / playerStats.maxShield;

            healthFill.fillAmount = Mathf.Clamp01(healthPercent);
            shieldFill.fillAmount = Mathf.Clamp01(shieldPercent);

            healthText.text = $"{(int)playerStats.currentHealth}+{(int)playerStats.currentShield} / {(int)playerStats.maxHealth}+{(int)playerStats.maxShield}";
        }
    }
}
