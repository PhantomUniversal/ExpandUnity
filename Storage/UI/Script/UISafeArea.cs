using UnityEngine;

namespace PhantomEngine
{
    [RequireComponent(typeof(RectTransform))]
    public class UISafeArea : MonoBehaviour
    {
        // Flag 처리
        [SerializeField] private UILocation uiLocation = UILocation.None;
        
        private void Awake()
        {
            if (!TryGetComponent(out RectTransform component))
                return;
            
            Rect safeArea = Screen.safeArea;
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            float minX = anchorMin.x;
            float minY = anchorMin.y;
            float maxX = anchorMax.x;
            float maxY = anchorMax.y;
            
            switch (uiLocation)
            {
                case UILocation.Top:
                    minX = 0f;
                    minY = 0f;
                    maxX = 1f;
                    break;

                case UILocation.Bottom:
                    minX = 0f;
                    maxX = 1f;
                    maxY = 1f;
                    break;

                case UILocation.Left:
                    minY = 0f;
                    maxY = 1f;
                    maxX = 1f;
                    break;

                case UILocation.Right:
                    minY = 0f;
                    maxY = 1f;
                    minX = 0f;
                    break;

                case UILocation.None:
                default:
                    break;
            }
            
            component.anchorMin = new Vector2(minX, minY);
            component.anchorMax = new Vector2(maxX, maxY);
        }
    }
}