using GameDesign;
using System;
using UnityEngine;

namespace GameDesign
{
    [Serializable]
    public class PlayerCombatData
    {
        [field: SerializeField] public PlayerATKDefinetionData ATKDefinetionData { get; private set; }
        [field: SerializeField] public PlayerAtkSettingData AtkSettingData { get; private set; }
    }
}
