using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace GameDesign
{
    public class FloatingTextManager : MonoBehaviour
    {
        public static FloatingTextManager Instance { get; private set; }

        [Header("Damage Text Settings")]
        public GameObject damageTextPrefab;   // 伤害数字预制体
        public Transform damageTextParent;    // 放置伤害数字的父对象（例如：Canvas）

        private void Awake()
        {
            // 单例模式，确保全局只有一个实例
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 在指定位置显示伤害数字
        /// </summary>
        /// <param name="worldPosition">伤害发生的世界坐标</param>
        /// <param name="damage">伤害数值</param>
        public void ShowFloatingText(Vector3 spawnPosition, float damage)
        {
            GameObject dmgText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, damageTextParent);
            // 调用 FloatingText 脚本设置文本内容
            dmgText.GetComponent<FloatingText>().SetText(damage.ToString());
        }
    }
}
