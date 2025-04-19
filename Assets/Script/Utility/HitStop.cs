using UnityEngine;
using System.Collections;

namespace GameDesign
{
    public class HitStop : MonoBehaviour
    {
        public static HitStop Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void StopTime(float duration, float slowFactor = 0.1f)
        {
            StartCoroutine(DoHitStop(duration, slowFactor));
        }

        private IEnumerator DoHitStop(float duration, float slowFactor)
        {
            Time.timeScale = slowFactor;  // ������Ϸ�ٶ�
            yield return new WaitForSecondsRealtime(duration); // **ʹ�� RealTime ȷ����ͣʱ�䲻���� Time.timeScale Ӱ��**
            Time.timeScale = 1f; // �ָ���Ϸ�ٶ�
        }
    }
}
