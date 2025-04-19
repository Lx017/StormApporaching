using UnityEngine;
using UnityEngine.Events;

namespace GameDesign
{
    public class PlayerStats : ActorStats
    {
        public bool weaponShow { get; set; } = false;
        public bool useMouse { get; set; } = false;
        public bool onJumping { get; set; } = false;
        public bool onAtk { get; set; } = false;
        public int atkCount { get; set; } = 0;
        static public float fireRateScale = 1f;
        static public float damageScale = 1f;
        static public int numBulletPerShot = 1;
        static public PlayerStats instance = null;

        private UnityAction<UpgradeOption> upgradeAppliedListener;

        protected override void Awake()
        {
            base.Awake();
            instance = this;
            upgradeAppliedListener = new UnityAction<UpgradeOption>(ApplyUpgrade);
        }
        static public Vector3 GetPlayerPosition()
        {
            if (instance != null)
            {
                return instance.transform.position;
            }
            else
            {
                Debug.LogWarning("PlayerStats instance is null!");
                return Vector3.zero;
            }
        }

        public void TakeDamage(float damage)
        {

            SoundEffectPlayer soundPlayer = FindObjectOfType<SoundEffectPlayer>();
            if (soundPlayer != null)
                soundPlayer.PlaySoundEffect("playergethit");

            base.TakeDamage(damage);        
        }
        public override void Die()
        {
            Debug.Log("Player Died! Show Game Over");
            EventManager.TriggerEvent<GameLoseEvent>();
        }
        private void ApplyUpgrade(UpgradeOption option)
        {
            switch (option.Type)
            {
                case UpgradeType.Attack:
                    AddAttackPower(option.Value);
                    break;
                case UpgradeType.MoveSpeed:
                    AddMoveSpeed(option.Value);
                    break;
                case UpgradeType.MaxHealth:
                    AddMaxHealth(option.Value);
                    break;
                case UpgradeType.HealthRegen:
                    AddHealthRegen(option.Value);
                    break;
                case UpgradeType.Shield:
                    AddShield(option.Value);
                    break;
                case UpgradeType.Defense:
                    AddDefense(option.Value);
                    break;
                case UpgradeType.CritRate:
                    AddCritRate(option.Value);
                    break;
                case UpgradeType.CritMultiplier:
                    AddCritMultiplier(option.Value);
                    break;
                case UpgradeType.AttackSpeed:
                    PlayerStats.fireRateScale *= 1.0f + option.Value;
                    //SetAttackSpeed(option.Value + attackSpeed);
                    break;
                case UpgradeType.BulletPerShot:
                    PlayerStats.numBulletPerShot += 1;
                    //SetAttackSpeed(option.Value + attackSpeed);
                    break;
            }

            Debug.Log($"Applied Upgrade: {option.Type} +{option.Value} ({option.Rarity})");
        }

        private void OnEnable()
        {
            EventManager.StartListening<UpgradeAppliedEvent, UpgradeOption>(upgradeAppliedListener);
        }

        private void OnDisable()
        {
            EventManager.StopListening<UpgradeAppliedEvent, UpgradeOption>(upgradeAppliedListener);
        }
    }
}
