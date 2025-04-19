using System.Collections;
using UnityEngine;

namespace GameDesign
{
    public class ArrowSpawn : MonoBehaviour
    {
        public GameObject arrowPrefab;
        public int arrowCount = 5;
        public float spawnHeight = 10f;
        public float delay = 0.1f;
        public float initialSpeed = 10f;
        private AudioSource audioSource;
        private bool triggered = false;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (!triggered && other.CompareTag("Player")) 
            {
                triggered = true;
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                StartCoroutine(SpawnArrows());
            }
        }

        IEnumerator SpawnArrows()
        {
            yield return new WaitForSeconds(delay);

            for (int i = 0; i < arrowCount; i++)
            {
                Vector3 spawnPosition = new Vector3(
                    transform.position.x + Random.Range(-1f, 1f),
                    transform.position.y + spawnHeight,
                    transform.position.z + Random.Range(-1f, 1f)
                );

                GameObject arrow = Instantiate(arrowPrefab, spawnPosition, Quaternion.Euler(-90f, 0f, 0f)); ;
                Rigidbody rb = arrow.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;
                    rb.linearVelocity = Vector3.down * initialSpeed;
                }
            }
        }
    }
}
