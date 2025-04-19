using UnityEngine;

namespace GameDesign
{
    public enum UpgradeType
    {
        Attack,
        MoveSpeed,
        MaxHealth,
        HealthRegen,
        Shield,
        Defense,
        CritRate,
        CritMultiplier,
        AttackSpeed,
        BulletPerShot
    }

    public class UpgradeOption
    {
        public UpgradeType Type;
        public float Value;
        public string Rarity;

        public UpgradeOption(UpgradeType type, float value, string rarity)
        {
            Type = type;
            Value = value;
            Rarity = rarity;
        }
    }
}
