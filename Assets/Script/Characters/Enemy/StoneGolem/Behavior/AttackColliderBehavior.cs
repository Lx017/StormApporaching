using UnityEngine;
using System.Collections.Generic;

namespace GameDesign
{
    public class AttackColliderBehavior : StateMachineBehaviour
    {
        public int[] damageAreaIDs; // �ܿص��˺����� ID �б�

        [Header("Attack Active Range")]
        public float startTime = 0.1f; // ��ʼʱ�䣨��һ�� 0~1��
        public float endTime = 0.9f;   // ����ʱ�䣨��һ�� 0~1��

        [Header("damage override (-1 means not override)")]
        public float overrideDamage = -1f;
        public float overrideKnockBackForce = -1f;
        public bool overrideKnockBack = false;
        public bool canOverrideKnockBack = false;

        // ͳһ���������˺�����
        public static Dictionary<int, List<EnemyDamageArea>> AllDamageAreas = new Dictionary<int, List<EnemyDamageArea>>();

        private bool hasEnabled = false;

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float normalizedTime = stateInfo.normalizedTime % 1; // ��һ��ʱ��

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
