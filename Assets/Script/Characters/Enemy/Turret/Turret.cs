using UnityEngine;

namespace GameDesign
{
    public class Turret : MonoBehaviour
    {
        public Transform target;
        public GameObject projectilePrefab;
        public Transform turretBase; // ������Y����ת
        public Transform turretHead; // ������X����ת��ʹZ���׼Ŀ��
        public float fireDelay = 2f;
        public float fireCooldown = 1.5f;
        public float lockDuration = 0.5f;

        public float baseRotationSpeed = 120f;
        public float headRotationSpeed = 90f;

        public float detectionRadius = 20f;
        public float laserRange = 100f;
        public float aimRadius = 1.5f;
        public float spreadAngle = 5f;
        public LayerMask targetMask;
        public Transform firePoint; // �ڿ�λ��
        public LineRenderer laserLine;

        private Transform currentTarget;
        private float aimTimer = 0f;
        private float cooldownTimer = 0f;
        private bool isLocked = false;
        private float lockTimer = 0f;
        


        void Update()
        {
            if (!isLocked)
            {
                currentTarget = FindTarget();
                target = currentTarget;
                RotateBase();
                RotateHead();
            }
            else
            {
                lockTimer -= Time.deltaTime;
                if (lockTimer <= 0f)
                {
                    isLocked = false;
                }
            }

            UpdateLaser();
        }

        void RotateBase()
        {
            if (target != null)
            {
                Vector3 dir = target.position - turretBase.position;
                dir.y = 0f; // ֻ��ˮƽ����ת

                if (dir.sqrMagnitude > 0.01f)
                {
                    Quaternion lookRot = Quaternion.LookRotation(dir);
                    turretBase.rotation = Quaternion.RotateTowards(turretBase.rotation, lookRot, baseRotationSpeed * Time.deltaTime);
                }
            }

                
        }

        void RotateHead()
        {
            if (target != null)
            {
                Vector3 worldDir = target.position - turretHead.position;
                Vector3 localDir = turretBase.InverseTransformDirection(worldDir);

                float desiredPitch = -Mathf.Atan2(localDir.y, localDir.z) * Mathf.Rad2Deg;

                float currentPitch = Mathf.DeltaAngle(0f, turretHead.localEulerAngles.x);
                float nextPitch = Mathf.MoveTowards(currentPitch, desiredPitch, headRotationSpeed * Time.deltaTime);
                nextPitch = Mathf.Clamp(nextPitch, -90f, 30f);

                turretHead.localRotation = Quaternion.Euler(nextPitch, 0f, 0f);
            }
            else
            {
                // ƽ����λ�� -90f X ��
                Quaternion targetRot = Quaternion.Euler(-90f, 0f, 0f);
                turretHead.localRotation = Quaternion.RotateTowards(turretHead.localRotation, targetRot, headRotationSpeed * Time.deltaTime);
            }

        }

        Transform FindTarget()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, targetMask);
            float closestDist = Mathf.Infinity;
            Transform closest = null;

            foreach (var hit in hits)
            {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = hit.transform;
                    closest = closest.Find("TargetPoint");
                }
            }

            return closest;
        }

        void UpdateLaser()
        {
            if (laserLine == null || firePoint == null) return;

            Vector3 start = firePoint.position;
            Vector3 direction = firePoint.forward;
            Vector3 end = start + direction * laserRange;

            laserLine.enabled = (target != null);
            laserLine.SetPosition(0, start);

            Ray ray = new Ray(start, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, laserRange))
            {
                end = hit.point;
            }
            laserLine.SetPosition(1, end);

            // �������Ƿ��ڼ���ĩ�����η�Χ��
            bool isAiming = Physics.CheckSphere(end, aimRadius, targetMask);

            if (isAiming && !isLocked)
            {
                aimTimer += Time.deltaTime;

                if (aimTimer >= fireDelay && cooldownTimer <= 0f)
                {
                    isLocked = true;
                    lockTimer = lockDuration;
                    cooldownTimer = fireCooldown;
                    aimTimer = 0f;
                    Invoke(nameof(FireProjectile), lockDuration * 0.8f);
                    Invoke(nameof(FireProjectile), lockDuration * 0.4f);
                }
            }
            else
            {
                aimTimer = 0f;
            }

            if (cooldownTimer > 0f)
                cooldownTimer -= Time.deltaTime;
        }

        void FireProjectile()
        {
            if (projectilePrefab != null && firePoint != null)
            {
                float randomNumber = Random.Range(0f, 1f);
                if (randomNumber < 0.5f)
                {
                    SoundEffectPlayer.play("blast1", 1f);
                }
                else
                {
                    SoundEffectPlayer.play("blast2", 1f);
                }
                // ԭʼ����
                Vector3 originalDir = firePoint.forward;

                // �������һ����΢ƫ��ķ���
                Quaternion randomRotation = Quaternion.Euler(
                    Random.Range(-spreadAngle, spreadAngle),
                    Random.Range(-spreadAngle, spreadAngle),
                    0f // ����Z���ȶ��������������ӷ���Ч��
                );

                Vector3 finalDir = randomRotation * originalDir;

                // ������ת�����Ŷ�����
                Quaternion projectileRotation = Quaternion.LookRotation(finalDir);

                Instantiate(projectilePrefab, firePoint.position, projectileRotation);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
