using UnityEngine;
using UnityEngine.UI;


namespace GameDesign
{
    public enum TargetType
    {
        Enemy,
        Chest// 可扩展：道具、任务点等
    }

    public class ArrowIndicator : MonoBehaviour
    {
        public Transform target;

        public TargetType targetType = TargetType.Enemy;
        public Transform playerTransform;
        public RectTransform arrowUI;

        public float maxDistanceFromCenter = 600f;
        public float minAlpha = 0.2f;
        public float maxAlpha = 1.0f;

        public float edgeBuffer = 50f;
        public float chestSearchRadius = 30f;

        private Image arrowImage;
        private Vector3 targetPos;

        private void Awake()
        {
            arrowImage = arrowUI.GetComponent<Image>();
        }

        private void Start()
        {
            if (playerTransform == null)
            {
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
                if (playerObj != null)
                {
                    playerTransform = playerObj.transform;
                }
                else
                {
                    Debug.LogWarning("[ArrowIndicator] No Player");
                }
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                targetType = targetType == TargetType.Enemy ? TargetType.Chest : TargetType.Enemy;
            }
            switch (targetType)
            {
                case TargetType.Enemy:
                    if (arrowImage != null)
                    {
                        Color c = arrowImage.color;
                        c = Color.red;
                        arrowImage.color = c;
                    }
                    var enemy = EnemyManager.Instance?.GetClosestEnemy(playerTransform.position);
                    target = enemy != null ? enemy.transform : null;
                    targetPos = enemy != null ? EnemyManager.Instance.GetEnemyPos(enemy) : new Vector3(0, 0, 0);
                    break;

                case TargetType.Chest:
                    arrowImage.color = Color.green;
                    target = ChestManager.Instance?.GetClosestChest(playerTransform.position, chestSearchRadius);
                    targetPos = target != null ? target.position : Vector3.zero;
                    break;

                default:
                    targetPos = target.position;
                    break;
            }
            if (target == null || playerTransform == null)
            {
                if (arrowImage != null) arrowImage.enabled = false;
                return;
            }
            else
            {
                if (arrowImage != null) arrowImage.enabled = true;
            }

            Vector3 screenPos = Camera.main.WorldToScreenPoint(targetPos);
            Vector3 playerScreenPos3D = Camera.main.WorldToScreenPoint(playerTransform.position);
            Vector2 playerCenter = new(playerScreenPos3D.x, playerScreenPos3D.y);

            bool isBehind = screenPos.z < 0;

            if (isBehind)
            {
                // 目标在背后，反向投影
                screenPos *= -1;
            }


            Vector2 safePos = (Vector2)screenPos;
            safePos.x = Mathf.Clamp(safePos.x, 0f + edgeBuffer, Screen.width - edgeBuffer);
            safePos.y = Mathf.Clamp(safePos.y, 0f + edgeBuffer, Screen.height - edgeBuffer);

            arrowUI.position = safePos;
            // 指向目标旋转
            Vector2 playerDir = (safePos - playerCenter).normalized;
            float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
            arrowUI.rotation = Quaternion.Euler(0, 0, angle - 90f);

            // 根据与“玩家屏幕位置”的距离调节透明度
            float distance = Vector2.Distance(safePos, playerCenter);
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, (distance - 100f) / maxDistanceFromCenter);
            alpha = Mathf.Clamp(alpha, minAlpha, maxAlpha);

            if (arrowImage != null)
            {
                Color c = arrowImage.color;
                c.a = alpha;
                arrowImage.color = c;
            }

        }

    }
}
