using UnityEngine;

namespace PhantomEngine
{
    public class UISafeArea : MonoBehaviour
    {
        private void Awake()
        {
            if (!TryGetComponent<RectTransform>(out var rectTransform)) 
                return;
            
            Rect safeArea = Screen.safeArea;
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}