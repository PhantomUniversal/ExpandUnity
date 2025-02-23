using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    [AddComponentMenu("UI/UIPopup")]
    [RequireComponent(typeof(Image))]
    public class UIPopup : UIPanel
    {
        public void SetShadow(bool enable)
        {
            if (!TryGetComponent(out Image component)) 
                return;
            
            var color = component.color;
            color.a = enable ? 160 : 0;
            component.color = color;
        }
    }
}