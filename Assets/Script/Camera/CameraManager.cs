using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace GameDesign
{
    public class CameraManager : MonoBehaviour
    {
        private UnityAction<bool> cameraLockListener;
        private GameObject Camera;


        private void Awake()
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("PlayerCamera"))
                {
                    Camera = child.gameObject;
                    break;
                }
                
                if (Camera == null)
                {
                    Debug.Log("Cannot find camera");
                }
            }


            cameraLockListener = new UnityAction<bool>(CameraLock);
        }


        private void OnEnable()
        {
            EventManager.StartListening<CameraLockEvent, bool>(cameraLockListener);
        }

        private void OnDisable()
        {
            EventManager.StopListening<CameraLockEvent, bool>(cameraLockListener);
        }

        private void CameraLock(bool lockCamera)
        {
            Camera.SetActive(!lockCamera);
        }
    }
}
