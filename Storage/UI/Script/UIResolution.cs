using UnityEngine;

namespace PhantomEngine
{
    [AddComponentMenu("UI/UIResolution")]
    [RequireComponent(typeof(RectTransform))]
    public class UIResolution : MonoBehaviour
    {
        [SerializeField] private Camera uiCamera;
        [SerializeField] private Vector2 uiSize = new(1920, 1080); 
        [SerializeField] private int uiFrame = 60;


        private void SetResolution(float width, float height)
        {
            uiSize.x = width;
            uiSize.y = height;
            UpdateResolution();
        }
        
        public void SetFrameRate(int value)
        {
            uiFrame = value;
            UpdateFrameRate();
        }
        
        
        private void Start()
        {
            SetResolution(uiSize.x, uiSize.y);
            SetFrameRate(uiFrame);
        }

        
        public void UpdateResolution()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            if (uiSize is { x: > 0f, y: > 0f })
            {
                int screenWidth = (int)uiSize.x;
                int screenHeight = (int)((Screen.height / (float)Screen.width) * uiSize.x);
                Screen.SetResolution(screenWidth, screenHeight, true);
            }
#endif
            
            if (!uiCamera || uiSize.x <= 0f || uiSize.y <= 0f) 
                return;

            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            float screenTarget = uiSize.x / screenSize.y;
            float screenAspect = screenSize.x / screenSize.y;
            
            if (Mathf.Abs(screenTarget - screenAspect) < 0.0001f)
            {
                // Full size
                uiCamera.rect = new Rect(0f, 0f, 1f, 1f);
            }
            else if (screenTarget < screenAspect)
            {
                float screenRatio = screenTarget / screenAspect;
                uiCamera.rect = new Rect((1f - screenRatio) * 0.5f, 0f, screenRatio, 1f);
            }
            else
            {
                float screenRatio = screenAspect / screenTarget;
                uiCamera.rect = new Rect(0f, (1f - screenRatio) * 0.5f, 1f, screenRatio);
            }
        }

        private void UpdateFrameRate()
        {
            if (uiFrame > 0)
            {
                Application.targetFrameRate = uiFrame;
            }
        }
    }
}