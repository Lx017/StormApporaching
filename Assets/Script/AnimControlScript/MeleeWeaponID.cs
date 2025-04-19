using UnityEngine;

namespace GameDesign
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class MeleeWeaponID : MonoBehaviour
    {
        [Tooltip("武器唯一ID，用于在动画行为中识别")]
        public int weaponID = 0;

        [HideInInspector]
        public CapsuleCollider weaponCollider;

        private void Awake()
        {
            weaponCollider = GetComponent<CapsuleCollider>();
        }
    }
}
