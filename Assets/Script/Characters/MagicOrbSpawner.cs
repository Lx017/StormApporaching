using UnityEngine;
using System.Collections.Generic;

namespace GameDesign
{
    public class MagicOrbSpawner : MonoBehaviour
    {
        public GameObject magicOrbPrefab;
        public Vector3 spawnOffset = new Vector3(0, 2, 0);
        public int maxOrbs = 1;

        private List<GameObject> currentOrbs = new();

        private void Update()
        {
            if (currentOrbs.Count < maxOrbs)
            {
                GameObject orb = Instantiate(magicOrbPrefab, transform.position + spawnOffset, Quaternion.identity, transform);
                currentOrbs.Add(orb);
            }

            // 清理已销毁的 orb
            currentOrbs.RemoveAll(o => o == null);
        }
    }
}
