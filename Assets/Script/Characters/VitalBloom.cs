using UnityEngine;

namespace GameDesign
{
    public class VitalBloom : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (CombatRewardManager.Instance != null)
            {
                CombatRewardManager.Instance.healthDropChance += 0.2f;
                Debug.Log("VitalBloom");
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
