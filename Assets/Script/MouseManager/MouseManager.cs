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
                UnlockCursor(); // ��֤���ʼ�տɼ�
                return; // ֱ�ӷ��أ���ִ�к����߼�
            }

            // ���û����������ʱ������겢��������Ϸ����
            if (Input.GetMouseButtonDown(0))
            {
                LockCursor();
            }

            // ���� Esc ��ʱ����������ʾ���
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnlockCursor();
            }
        }

        // �������������
        void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked; // ������꣬ʹ�䲻���Ƴ�����
            Cursor.visible = false; // �������
            isCursorLocked = true;
        }

        // ��������ʾ���
        void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None; // �ͷ����
            Cursor.visible = true; // ��ʾ���
            isCursorLocked = false;
        }
    }
}
