using UnityEngine;

namespace GameDesign
{
    public class TwinTrigger : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            var stats = GetComponentInParent<PlayerStats>();
            if (stats != null)
            {
                PlayerStats.numBulletPerShot += 1;
                Debug.Log("[TwinTrigger] Applied: +1 bullet per shot.");
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
