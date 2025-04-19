using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace GameDesign
{
    public class FloatingTextManager : MonoBehaviour
    {
        public static FloatingTextManager Instance { get; private set; }

        [Header("Damage Text Settings")]
        public GameObject damageTextPrefab;   // �˺�����Ԥ����
        public Transform damageTextParent;    // �����˺����ֵĸ��������磺Canvas��

        private void Awake()
        {
            // ����ģʽ��ȷ��ȫ��ֻ��һ��ʵ��
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
        /// ��ָ��λ����ʾ�˺�����
        /// </summary>
        /// <param name="worldPosition">�˺���������������</param>
        /// <param name="damage">�˺���ֵ</param>
        public void ShowFloatingText(Vector3 spawnPosition, float damage)
        {
            GameObject dmgText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, damageTextParent);
            // ���� FloatingText �ű������ı�����
            dmgText.GetComponent<FloatingText>().SetText(damage.ToString());
        }
    }
}
