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
            Time.timeScale = slowFactor;  // 降低游戏速度
            yield return new WaitForSecondsRealtime(duration); // **使用 RealTime 确保暂停时间不会受 Time.timeScale 影响**
            Time.timeScale = 1f; // 恢复游戏速度
        }
    }
}
