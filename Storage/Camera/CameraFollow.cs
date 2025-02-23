using UnityEngine;

namespace PhantomEngine
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform cameraTarget;
        [SerializeField] private float cameraDistance = 10.0f;
        [SerializeField] private float cameraHeight = 5.0f;
        [SerializeField] private float cameraRotateSpeed = 5.0f;

        private Transform cameraTransform;
        
        private void Start()
        {
            cameraTransform = GetComponent<Transform>();
        }

        void LateUpdate()
        {
            float angle = Mathf.LerpAngle(cameraTransform.eulerAngles.y, cameraTarget.eulerAngles.y, cameraRotateSpeed * Time.deltaTime);
            Quaternion rotation = Quaternion.Euler(0f, angle, 0);
            cameraTransform.position = cameraTarget.position - (rotation * Vector3.forward * cameraDistance) + (Vector3.up * cameraHeight);
            cameraTransform.LookAt(cameraTarget);
        }
    }
}