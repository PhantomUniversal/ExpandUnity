using UnityEngine;

namespace PhantomEngine
{
    [AddComponentMenu("UI/UIPanel")]
    [RequireComponent(typeof(CanvasGroup), typeof(Animator))]
    public class UIPanel : MonoBehaviour
    {
        public void SetAlpha(float alpha)
        {
            if (!TryGetComponent(out CanvasGroup component))
                return;
            
            component.alpha = alpha;
        }
        
        public void SetPlay(int hash)
        {
            if (!TryGetComponent(out Animator component)) 
                return;
            
            if (component.runtimeAnimatorController == null)
                return;
                
            component.Play(hash);
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