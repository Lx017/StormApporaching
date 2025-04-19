using UnityEngine;

namespace GameDesign
{
    public class AirWallController : MonoBehaviour
    {

        private bool playerTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerTriggered = true;
                CheckEnemies();
            }
        }

        private void CheckEnemies()
        {
            // 检查场景中是否还有敌人
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy == null)
            {
                Destroy(gameObject); // 移除空气墙
            }
        }
    }
}

