using UnityEngine;
using System.Collections.Generic;

namespace GameDesign
{
    public class AttackColliderBehavior : StateMachineBehaviour
    {
        public int[] damageAreaIDs; // 受控的伤害区域 ID 列表

        [Header("Attack Active Range")]
        public float startTime = 0.1f; // 开始时间（归一化 0~1）
        public float endTime = 0.9f;   // 结束时间（归一化 0~1）

        [Header("damage override (-1 means not override)")]
        public float overrideDamage = -1f;
        public float overrideKnockBackForce = -1f;
        public bool overrideKnockBack = false;
        public bool canOverrideKnockBack = false;

        // 统一管理所有伤害区域
        public static Dictionary<int, List<EnemyDamageArea>> AllDamageAreas = new Dictionary<int, List<EnemyDamageArea>>();

        private bool hasEnabled = false;

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float normalizedTime = stateInfo.normalizedTime % 1; // 归一化时间

            if (!hasEnabled && normalizedTime >= startTime && normalizedTime <= endTime)
            {
                SetCollidersActive(true);
                hasEnabled = true;
            }

            if (hasEnabled && normalizedTime > endTime)
            {
                SetCollidersActive(false);
                hasEnabled = false;
            }
        }

        private void SetCollidersActive(bool state)
        {
            foreach (int id in damageAreaIDs)
            {
                if (AllDamageAreas.ContainsKey(id))
                {
                    foreach (EnemyDamageArea area in AllDamageAreas[id])
                    {
                        if (state)
                        {
                            if (overrideDamage >= 0) area.baseDamage = overrideDamage;
                            if (overrideKnockBackForce >= 0) area.knockBackForce = overrideKnockBackForce;
                            if (canOverrideKnockBack) area.canKnockBack = overrideKnockBack;

                            area.EnableCollider();
                        }
                        else
                        {
                            area.DisableCollider();
                        }
                    }
                }
            }
        }
    }
}
