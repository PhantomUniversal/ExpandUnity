using UnityEngine;
using UnityEngine.EventSystems;

namespace PhantomEngine
{
    public class UIPointer : MonoBehaviour
    {
        private void Update()
        {
            // Mouse
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    
                }
            }
            
            // Touch
            if (Input.touchCount > 0)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    
                }
            }
        }
    }
}