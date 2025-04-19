using System.Collections;
using UnityEngine;

namespace GameDesign
{
    public class PlayerCombatStateReusableData
    {
        public bool OnCombatOne { get; set; } = false;
        public bool OnCombatTwo { get; set; } = false;
        public int CombatCount { get; set; } = 0;
    }
}