using UnityEngine;
using System.Collections.Generic;

namespace GameDesign
{
    public class ChestManager : MonoBehaviour
    {
        public static ChestManager Instance { get; private set; }

        private HashSet<Transform> chests = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        /// <summary>
        /// ×¢²á±¦Ïä
        /// </summary>
        public void RegisterChest(Transform chest)
        {
            if (chest != null)
                chests.Add(chest);
        }

        /// <summary>
        /// ×¢Ïú±¦Ïä
        /// </summary>
        public void UnregisterChest(Transform chest)
        {
            if (chest != null)
                chests.Remove(chest);
        }

        /// <summary>
        /// ²éÕÒÀë origin ×î½üµÄ±¦Ïä
        /// </summary>
        public Transform GetClosestChest(Vector3 origin, float radius)
        {
            float minDist = Mathf.Infinity;
            Transform closest = null;

            foreach (var chest in chests)
            {
                if (chest == null) continue;

                float dist = Vector3.Distance(origin, chest.position);
                if (dist <= radius && dist < minDist)
                {
                    minDist = dist;
                    closest = chest;
                }
            }

            return closest;
        }

        public bool HasChests()
        {
            return chests.Count > 0;
        }
    }
}
