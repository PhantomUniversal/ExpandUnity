using UnityEngine;

namespace PhantomEngine
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float zoomSpeed = 0.1f;
        [SerializeField] private float zoomMin = 5f;
        [SerializeField] private float zoomMax = 20f;
        
        private Camera zoomCamera;


        void Start()
        {
            zoomCamera = Camera.main;            
        }

        private void Update()
        {
            if (Input.touchCount == 2)
            {
                Scroll();
            }
        }


        private void Scroll()
        {
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scrollDelta) > 0.01f)
            {
                if (zoomCamera.orthographic)
                {
                    zoomCamera.orthographicSize -= scrollDelta * zoomSpeed;
                    zoomCamera.orthographicSize = Mathf.Clamp(zoomCamera.orthographicSize, zoomMin, zoomMax);
                }
                else
                {
                    zoomCamera.fieldOfView -= scrollDelta * zoomSpeed;
                    zoomCamera.fieldOfView = Mathf.Clamp(zoomCamera.fieldOfView, zoomMin, zoomMax);
                }
            }
        }
        
        
        private void Touch()
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // 이전 프레임에서의 터치 위치 계산
            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            // 이전 프레임과 현재 프레임의 터치 간 거리 계산
            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
            float currentTouchDeltaMag = (touch0.position - touch1.position).magnitude;

            // 터치 간 거리 변화량 (양수이면 줌 인, 음수이면 줌 아웃)
            float deltaMagnitudeDiff = prevTouchDeltaMag - currentTouchDeltaMag;

            // 카메라 모드에 따른 줌 처리
            if (zoomCamera.orthographic)
            {
                zoomCamera.orthographicSize += deltaMagnitudeDiff * zoomSpeed;
                zoomCamera.orthographicSize = Mathf.Clamp(zoomCamera.orthographicSize, zoomMin, zoomMax);
            }
            else
            {
                zoomCamera.fieldOfView += deltaMagnitudeDiff * zoomSpeed;
                zoomCamera.fieldOfView = Mathf.Clamp(zoomCamera.fieldOfView, zoomMin, zoomMax);
            }
        }
    }
}