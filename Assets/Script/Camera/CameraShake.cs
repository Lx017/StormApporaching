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
                Debug.LogError("CinemachineImpulseSource δ�ҵ�����ȷ�����ѹ��ص� PlayerCamera �ϣ�");
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
                Debug.LogWarning("δ�ҵ� CinemachineImpulseSource���޷������𶯣�");
            }
        }
    }
}
