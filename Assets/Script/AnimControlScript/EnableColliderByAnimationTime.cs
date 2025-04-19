using UnityEngine;

namespace GameDesign
{
    public class EnableMeleeWeaponByID : StateMachineBehaviour
    {
        [Tooltip("动画中激活的目标武器ID")]
        public int weaponIDToActivate = 0;

        [Range(0f, 1f)]
        public float startTime = 0.1f;

        [Range(0f, 1f)]
        public float endTime = 0.9f;

        private MeleeWeaponID matchedWeapon;
        private bool initialized = false;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (initialized) return;

            // 搜索所有带有 "Weapon" 标签的子物体
            Transform[] children = animator.transform.GetComponentsInChildren<Transform>(true);
            foreach (var child in children)
            {
                if (child.CompareTag("Weapon"))
                {
                    MeleeWeaponID weapon = child.GetComponent<MeleeWeaponID>();
                    if (weapon != null && weapon.weaponID == weaponIDToActivate)
                    {
                        matchedWeapon = weapon;
                        break;
                    }
                }
            }

            if (matchedWeapon != null)
            {
                matchedWeapon.enabled = false;
                if (matchedWeapon.weaponCollider != null)
                    matchedWeapon.weaponCollider.enabled = false;
            }

            initialized = true;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (matchedWeapon == null) return;

            float time = stateInfo.normalizedTime % 1f;
            bool active = time >= startTime && time <= endTime;

            matchedWeapon.enabled = active;
            if (matchedWeapon.weaponCollider != null)
                matchedWeapon.weaponCollider.enabled = active;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (matchedWeapon == null) return;

            matchedWeapon.enabled = false;
            if (matchedWeapon.weaponCollider != null)
                matchedWeapon.weaponCollider.enabled = false;
        }
    }
}
