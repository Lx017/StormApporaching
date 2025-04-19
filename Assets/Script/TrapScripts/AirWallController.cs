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
            // ��鳡�����Ƿ��е���
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy == null)
            {
                Destroy(gameObject); // �Ƴ�����ǽ
            }
        }
    }
}

