using System;
using UnityEngine;

namespace GameDesign
{
    public class rockWallWatcher : MonoBehaviour
    {

        Animator animator;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            animator = GetComponent<Animator>();


        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnEnable()
        {
            EventManager.StartListening<TutorialEndEvent>(openWall);
        }


        private void OnDisable()
        {
            EventManager.StopListening<TutorialEndEvent>(openWall);
        }

        private void openWall()
        {
            animator.SetTrigger("open");
        }
    }
}
