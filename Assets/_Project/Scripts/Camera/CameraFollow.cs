using UnityEngine;

namespace Subway.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [Header("Смещение по высоте")]
        public float OffsetY;

        [Header("Наклон камеры")]
        public float RotationAngleX;
        public float RotationAngleY;

        [Header("Расстояние от игрока")]
        public float Distance;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Обновление движения камеры в конце кадра
        private void LateUpdate()
        {
            if (_target == null)
                return;

            FollowTarget();
        }

        private void FollowTarget()
        {
            Quaternion rotation = Quaternion.Euler(RotationAngleX, RotationAngleY, 0f);
            Vector3 position = rotation * new Vector3(0f, 0f, -Distance) + FollowingPointPosition();

            transform.SetPositionAndRotation(position, rotation);
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 position = _target.position;
            position.y += OffsetY;

            return position;
        }

        public void SetTarget(Transform cameraTarget)
             => _target = cameraTarget;
    }
}
