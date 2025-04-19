using UnityEngine;
using TMPro;

namespace GameDesign
{
    public class FloatingText : MonoBehaviour
    {
        public float floatSpeed = 1f;      // 向上移动速度
        public float lifeTime = 1f;        // 持续时间
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
                // 让文本面向摄像机，同时可以根据需求做旋转调整（例如旋转180度）
                transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
            }

            // 向上移动
            transform.position += Vector3.up * floatSpeed * Time.deltaTime;

            // 根据剩余时间调整透明度，实现淡出效果
            lifeTime -= Time.deltaTime;
            if (lifeTime > 0)
            {
                Color newColor = originalColor;
                newColor.a = Mathf.Clamp01(lifeTime);  // 当lifeTime为1时全透明，0时完全显示，这里可根据需求调整
                text.color = newColor;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // 设置显示的数字
        public void SetText(string message)
        {
            text.text = message;
        }
    }
}
