using UnityEngine;
using TMPro;

namespace GameDesign
{
    public class FloatingText : MonoBehaviour
    {
        public float floatSpeed = 1f;      // �����ƶ��ٶ�
        public float lifeTime = 1f;        // ����ʱ��
        private TextMeshProUGUI text;
        private Color originalColor;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            originalColor = text.color;
        }

        private void Update()
        {
            if (Camera.main != null)
            {
                // ���ı������������ͬʱ���Ը�����������ת������������ת180�ȣ�
                transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
            }

            // �����ƶ�
            transform.position += Vector3.up * floatSpeed * Time.deltaTime;

            // ����ʣ��ʱ�����͸���ȣ�ʵ�ֵ���Ч��
            lifeTime -= Time.deltaTime;
            if (lifeTime > 0)
            {
                Color newColor = originalColor;
                newColor.a = Mathf.Clamp01(lifeTime);  // ��lifeTimeΪ1ʱȫ͸����0ʱ��ȫ��ʾ������ɸ����������
                text.color = newColor;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // ������ʾ������
        public void SetText(string message)
        {
            text.text = message;
        }
    }
}
