using UnityEngine;

namespace GameDesign
{
    public abstract class WeaponBase : MonoBehaviour
    {
        //�����˺�
        public float baseDamage = 0f;

        public float knockBackForce = 0f;

        public bool canKnockBack = false;

        //�˺����ͣ�Ŀǰ����ǿ��Էֳ��������ħ���˺���������Ҫ�������ٸ�
        public string damageType = "Physical";

        //public ActorStats owner;

        /// <summary>
        /// �ⲿ���ô˷������й�����
        /// ������ڴ�֮ǰ��һЩͨ�ô�����������Ч����ħ�ȣ����ٵ��� PerformAttack��
        /// </summary>
        public virtual void Attack()
        {
            PerformAttack();
        }

        /// <summary>
        /// ����ʵ�֣���ͬ�����Ĺ�����ʽ����ս��Զ�̡�AOE�ȣ��������д
        /// </summary>
        protected virtual void PerformAttack()
        {
            return;
        }

        /// <summary>
        /// ���������˺���Ĭ�Ͻ����� baseDamage��
        /// ����Ҫ��ɫ�������򱩻������ݣ�����������ⲿ���� owner �� ActorStats �������Ӽ��㡣
        /// </summary>
        public virtual float CalculateDamage()
        {
            // �����˺� + (��ѡ) ��ɫ�ӳ�
            float totalDamage = baseDamage;

            // ���������������ӽ�ɫ�Ĺ�����/�����ȣ��������� owner.ActorStats ��ȡ
            // ���磺 totalDamage += owner.attackPower;
            //        if (owner.IsCriticalHit()) totalDamage *= owner.critMultiplier;

            return totalDamage;
        }
    }
}
