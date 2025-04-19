using UnityEngine;

namespace GameDesign
{
    public class GameNextTrigger : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.TriggerEvent<GameWinEvent>();
            }
        }
    }
}
