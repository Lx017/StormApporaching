using UnityEngine;
using UnityEngine.UI;
namespace GameDesign
{
    public class HealthBarUI : MonoBehaviour
    {
        public ActorStats target; // 要跟踪的怪物
        public Vector3 offset; // 血条相对怪物的位置
        private Slider slider;
        private Camera mainCamera;

        private float GetTargetHeight()
        {
            Renderer[] renderers = target.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0) return 2f; // 默认值

            Bounds combinedBounds = renderers[0].bounds;
            foreach (Renderer rend in renderers)
            {
                combinedBounds.Encapsulate(rend.bounds);
            }
            return combinedBounds.size.y;
        }

        void Start()
        {
            slider = GetComponentInChildren<Slider>();
            mainCamera = Camera.main;

            if (target == null)
            {
                Debug.LogWarning("No ActorStats binded to HealthBar！");
            }
            offset = new Vector3(0, GetTargetHeight() + 0.5f, 0);
        }

        void Update()
        {
            if (target == null) return;

            // 更新位置
            transform.position = target.transform.position + offset;

            // 可选：让血条面向摄像机
            transform.forward = mainCamera.transform.forward;

            if(target.currentHealth < 1)
            {
                Destroy(gameObject);
            }

            // 更新血条滑动值
            if (slider != null)
            {
                slider.value = target.currentHealth / target.maxHealth;
            }
        }
    }
}