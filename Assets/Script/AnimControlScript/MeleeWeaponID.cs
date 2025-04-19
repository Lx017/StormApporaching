using UnityEngine;

namespace GameDesign
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class MeleeWeaponID : MonoBehaviour
    {
        [Tooltip("����ΨһID�������ڶ�����Ϊ��ʶ��")]
        public int weaponID = 0;

        [HideInInspector]
        public CapsuleCollider weaponCollider;

        private void Awake()
        {
            weaponCollider = GetComponent<CapsuleCollider>();
        }
    }
}
