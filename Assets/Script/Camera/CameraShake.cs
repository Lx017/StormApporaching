using UnityEngine;
using Unity.Cinemachine;

namespace GameDesign
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance;
        private CinemachineImpulseSource impulseSource;

        private void Awake()
        {
            Instance = this;
            impulseSource = GetComponent<CinemachineImpulseSource>();

            if (impulseSource == null)
            {
                Debug.LogError("CinemachineImpulseSource 未找到，请确保它已挂载到 PlayerCamera 上！");
            }
        }

        public void Shake(float force = 1.0f)
        {
            if (impulseSource != null)
            {
                impulseSource.GenerateImpulseWithForce(force);
            }
            else
            {
                Debug.LogWarning("未找到 CinemachineImpulseSource，无法触发震动！");
            }
        }
    }
}
