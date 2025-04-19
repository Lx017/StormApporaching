using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JUTPSEditor.JUHeader;
using JUTPS.InventorySystem;
namespace JUTPS.WeaponSystem
{
    public class MeleeWeapon : JUTPS.ItemSystem.JUHoldableItem
    {
        [JUHeader("Melee Weapon Settings")]
        public string AttackAnimatorParameterName = "OneHandMeleeAttack";
        public Damager DamagerToEnable;

        [JUHeader("Damage Settings")]
        public bool EnableHealthLoss;
        public float MeleeWeaponHealth = 100;
        public float DamagePerUse = 1;

        protected override void Start()
        {
            base.Start();
            DamagerToEnable = DamagerToEnable ?? GetComponentInChildren<Damager>();
        }

    }

}
