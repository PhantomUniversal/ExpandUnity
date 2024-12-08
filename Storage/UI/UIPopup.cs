using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    [RequireComponent(typeof(CanvasGroup), typeof(Image), typeof(Animator))]
    public class UIPopup : MonoBehaviour
    {
        public void SetAlpha(float alpha)
        {
            if (!TryGetComponent(out CanvasGroup component))
                return;
            
            component.alpha = alpha;
        }
        
        public void SetShadow(bool enable)
        {
            if (!TryGetComponent(out Image component)) 
                return;
            
            var color = component.color;
            color.a = enable ? 160 : 0;
            component.color = color;
        }
        
        public void SetPlay(int id)
        {
            if (!TryGetComponent(out Animator component)) 
                return;
            
            if (component.runtimeAnimatorController == null)
                return;
                
            component.Play(id);
        }
        
        public void SetPause()
        {
            if (!TryGetComponent(out Animator component)) 
                return;
            
            if (component.runtimeAnimatorController == null)
                return;
                
            component.Rebind();
            component.Update(0f);
        }
    }
}