using UnityEngine;
using UnityEngine.UI;
namespace GameDesign
{
    public class HealthBarUI : MonoBehaviour
    {
        public ActorStats target; // Ҫ���ٵĹ���
        public Vector3 offset; // Ѫ����Թ����λ��
        private Slider slider;
        private Camera mainCamera;

        private float GetTargetHeight()
        {
            Renderer[] renderers = target.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0) return 2f; // Ĭ��ֵ

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
                Debug.LogWarning("No ActorStats binded to HealthBar��");
            }
            offset = new Vector3(0, GetTargetHeight() + 0.5f, 0);
        }

        void Update()
        {
            if (target == null) return;

            // ����λ��
            transform.position = target.transform.position + offset;

            // ��ѡ����Ѫ�����������
            transform.forward = mainCamera.transform.forward;

            if(target.currentHealth < 1)
            {
                Destroy(gameObject);
            }

            // ����Ѫ������ֵ
            if (slider != null)
            {
                slider.value = target.currentHealth / target.maxHealth;
            }
        }
    }
}