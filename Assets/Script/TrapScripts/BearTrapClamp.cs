using UnityEngine;

namespace GameDesign
{
    public class BearTrapClamp : MonoBehaviour
    {
        public AudioSource audioSource;
        private Animator animator;
        private bool isTriggered = false;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (!isTriggered && other.CompareTag("Player"))
            {
                isTriggered = true;
                animator.SetTrigger("BearTrap_ClampTrigger");
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                Invoke("ResetTrap", 3f);
            }
        }

        void ResetTrap()
        {
            isTriggered = false;
        }
    }

}