using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PhantomEngine
{
    [RequireComponent(typeof(ScrollRect))]
    public class UIScroll : MonoBehaviour, IDragHandler
    {
        private ScrollRect scrollRect;
        private UIDirection scrollDirection = UIDirection.None;
        private float scrollSpeed = 0.1f;
        
        
        public bool IsAuto { get; private set; }

        
        public void SetAuto(bool enable)
        {
            if (scrollDirection == UIDirection.None)
                return;
            
            IsAuto = enable;
        }

        public void SetReset(float value = 0f)
        {
            IsAuto = false;
            switch (scrollDirection)
            {
                case UIDirection.Vertical:
                    scrollRect.verticalNormalizedPosition = value;
                    break;
                case UIDirection.Horizontal:
                    scrollRect.horizontalNormalizedPosition = value;
                    break;
                case UIDirection.None:
                default:
                    scrollRect.verticalNormalizedPosition = value;
                    scrollRect.horizontalNormalizedPosition = value;
                    return;
            }
        }
        
        public void SetSpeed(float speed)
        {
            scrollSpeed = speed;
        }
        
        public void SetDirection(UIDirection direction)
        {
            scrollDirection = direction;
            scrollRect.vertical = scrollDirection == UIDirection.Vertical;
            scrollRect.horizontal = scrollDirection == UIDirection.Horizontal;
        }
        
        
        private void Start()
        {
            if (!TryGetComponent(out ScrollRect component))
            {
                enabled = false;
                return;
            }
            
            scrollRect = component;
            scrollRect.onValueChanged.AddListener(OnValueChanged);
            
            CheckDirection();
        }

        private void Update()
        {
            if (!IsAuto)
                return;

            if (scrollDirection == UIDirection.None)
            {
                IsAuto = false;
                return;
            }
            
            switch (scrollDirection)
            {
                case UIDirection.Vertical:
                    scrollRect.verticalNormalizedPosition = Mathf.Lerp(scrollRect.verticalNormalizedPosition, 1f, Time.deltaTime * scrollSpeed);
                    if (scrollRect.verticalNormalizedPosition >= 0.9999f)
                    {
                        scrollRect.verticalNormalizedPosition = 1f;
                        IsAuto = false;
                    }
                    break;
                case UIDirection.Horizontal:
                    scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, 1f, Time.deltaTime * scrollSpeed);
                    if (scrollRect.horizontalNormalizedPosition >= 1f)
                    {
                        scrollRect.horizontalNormalizedPosition = 1f;
                        IsAuto = false;
                    }
                    break;
            }
        }

        private void OnDestroy()
        {
            if (scrollRect != null)
            {
                scrollRect.onValueChanged.RemoveListener(OnValueChanged);   
            }
        }


        private void OnValueChanged(Vector2 value)
        {
            switch (scrollDirection)
            {
                case UIDirection.Vertical:
                    float clampedY = Mathf.Clamp(value.y, 0f, 1f);
                    if (!Mathf.Approximately(clampedY, value.y))
                    {
                        scrollRect.verticalNormalizedPosition = clampedY;
                    }
                    break;
                case UIDirection.Horizontal:
                    float clampedX = Mathf.Clamp(value.x, 0f, 1f);
                    if (!Mathf.Approximately(clampedX, value.x))
                    {
                        scrollRect.horizontalNormalizedPosition = clampedX;
                    }
                    break;
            }
        }

        
        public void CheckDirection()
        {
            if (scrollDirection == UIDirection.None)
            {
                if (scrollRect.vertical && !scrollRect.horizontal)
                    scrollDirection = UIDirection.Vertical;
                else if (!scrollRect.vertical && scrollRect.horizontal)
                    scrollDirection = UIDirection.Horizontal;
                else
                    scrollDirection = UIDirection.None;
            }
        }
        
        
        public void OnDrag(PointerEventData eventData)
        {
            IsAuto = false;
        }
    }
}