using UnityEngine;
using TMPro;

namespace GameDesign
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text attackText;
        [SerializeField] private TMP_Text maxHealthText;
        [SerializeField] private TMP_Text curHealthText;
        [SerializeField] private TMP_Text moveSpeedText;
        [SerializeField] private TMP_Text shieldText;
        //[SerializeField] private TMP_Text attackSpeedText;

        private PlayerStats player;
        private PlayerExperience playerExp;

        private void Start()
        {
            player = FindFirstObjectByType<PlayerStats>();
            playerExp = FindFirstObjectByType<PlayerExperience>();
            UpdateStats();
        }

        private void Update()
        {
            UpdateStats();
        }

        private void UpdateStats()
        {
            if (player != null)
            {
                levelText.text = $"Level: {playerExp.currentLevel}";
                attackText.text = $"Attack: {PlayerStats.damageScale:F1}";
                maxHealthText.text = $"MaxHealth: {player.maxHealth:F1}";
                curHealthText.text = $"CurHealth: {player.currentHealth:F1}";
                moveSpeedText.text = $"MoveSpeed: {player.moveSpeed:F1}";
                shieldText.text = $"Shield: {player.currentShield:F1}";
                //attackSpeedText.text = $"Attack Speed: {PlayerStats.fireRateScale:F1}";
            }
        }
    }
}
