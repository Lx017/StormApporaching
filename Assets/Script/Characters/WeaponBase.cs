using UnityEngine;

namespace GameDesign
{
    public abstract class WeaponBase : MonoBehaviour
    {
        //基础伤害
        public float baseDamage = 0f;

        public float knockBackForce = 0f;

        public bool canKnockBack = false;

        //伤害类型，目前想的是可以分成物理或者魔法伤害，但不重要，后面再改
        public string damageType = "Physical";

        //public ActorStats owner;

        /// <summary>
        /// 外部调用此方法进行攻击，
        /// 子类可在此之前做一些通用处理（动画、音效、耗魔等），再调用 PerformAttack。
        /// </summary>
        public virtual void Attack()
        {
            PerformAttack();
        }

        /// <summary>
        /// 子类实现：不同武器的攻击方式（近战、远程、AOE等）在这里编写
        /// </summary>
        protected virtual void PerformAttack()
        {
            return;
        }

        /// <summary>
        /// 计算最终伤害，默认仅返回 baseDamage。
        /// 若需要角色攻击力或暴击等数据，可由子类或外部引用 owner 的 ActorStats 再做附加计算。
        /// </summary>
        public virtual float CalculateDamage()
        {
            // 基础伤害 + (可选) 角色加成
            float totalDamage = baseDamage;

            // 如果你想在这里叠加角色的攻击力/暴击等，可以再向 owner.ActorStats 获取
            // 比如： totalDamage += owner.attackPower;
            //        if (owner.IsCriticalHit()) totalDamage *= owner.critMultiplier;

            return totalDamage;
        }
    }
}
