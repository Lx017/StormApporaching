using UnityEngine;

namespace GameDesign
{
    [RequireComponent(typeof(Player))]
    public class MouseManager : MonoBehaviour
    {
        private Player player;

        private bool isCursorLocked = false;



        private void Awake()
        {
            player = GetComponent<Player>();
        }

        void Update()
        {
            if (player.ReusableData.UseMouse)
            {
                UnlockCursor(); // 保证鼠标始终可见
                return; // 直接返回，不执行后续逻辑
            }

            // 当用户点击鼠标左键时隐藏鼠标并锁定到游戏窗口
            if (Input.GetMouseButtonDown(0))
            {
                LockCursor();
            }

            // 按下 Esc 键时，解锁并显示鼠标
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnlockCursor();
            }
        }

        // 锁定并隐藏鼠标
        void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标，使其不能移出窗口
            Cursor.visible = false; // 隐藏鼠标
            isCursorLocked = true;
        }

        // 解锁并显示鼠标
        void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None; // 释放鼠标
            Cursor.visible = true; // 显示鼠标
            isCursorLocked = false;
        }
    }
}
