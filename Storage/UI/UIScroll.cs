using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PhantomEngine
{
    [RequireComponent(typeof(ScrollRect))]
    public class UIScroll : MonoBehaviour, IDragHandler
    {
        public enum ScrollDirection
        {
            None,
            Vertical,
            Horizontal
        }

        private bool scrollAuto;
        private ScrollRect scrollRect;
        private ScrollDirection scrollDirection = ScrollDirection.None;
        
        public void SetAuto()
        {
            scrollAuto = true;
        }

        public void SetReset()
        {
            switch (scrollDirection)
            {
                case ScrollDirection.Vertical:
                    scrollRect.verticalNormalizedPosition = 0;
                    break;
                case ScrollDirection.Horizontal:
                    scrollRect.horizontalNormalizedPosition = 0;
                    break;
            }
        }
        
        
        private void Start()
        {
            if (!TryGetComponent(out ScrollRect component)) 
                return;
            
            scrollRect = component;
            scrollRect.onValueChanged.AddListener(OnValueChanged);
            
            if (scrollRect.vertical && !scrollRect.horizontal)
            {
                scrollDirection = ScrollDirection.Vertical;
            }
            else if (!scrollRect.vertical && scrollRect.horizontal)
            {
                scrollDirection = ScrollDirection.Horizontal;
            }
            else
            {
                scrollDirection = ScrollDirection.None;
            }
        }

        private void FixedUpdate()
        {
            if (!scrollAuto)
                return;
            
            switch (scrollDirection)
            {
                case ScrollDirection.None:
                    scrollAuto = false;
                    break;
                case ScrollDirection.Vertical:
                    scrollRect.verticalNormalizedPosition += Time.deltaTime * 0.1f;
                    if (scrollRect.verticalNormalizedPosition >= 1)
                    {
                        scrollAuto = false;
                    }
                    break;
                case ScrollDirection.Horizontal:
                    scrollRect.horizontalNormalizedPosition += Time.deltaTime * 0.1f;
                    if (scrollRect.horizontalNormalizedPosition >= 1)
                    {
                        scrollAuto = false;
                    }
                    break;
            }
        }

        private void OnDestroy()
        {
            if (scrollRect == null)
                return;
            
            scrollRect.onValueChanged.RemoveListener(OnValueChanged);
        }


        private void OnValueChanged(Vector2 value)
        {
            switch (scrollDirection)
            {
                case ScrollDirection.Vertical:
                    scrollRect.verticalNormalizedPosition = value.y switch
                    {
                        > 1 => 1,
                        < 0 => 0,
                        _ => scrollRect.verticalNormalizedPosition
                    };
                    break;
                case ScrollDirection.Horizontal:
                    scrollRect.horizontalNormalizedPosition = value.x switch
                    {
                        > 1 => 1,
                        < 0 => 0,
                        _ => scrollRect.horizontalNormalizedPosition
                    };
                    break;
            }
        }

        
        public void OnDrag(PointerEventData eventData)
        {
            scrollAuto = false;
        }
    }
}