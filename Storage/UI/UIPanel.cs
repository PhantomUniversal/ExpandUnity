using UnityEngine;

namespace PhantomEngine
{
    [RequireComponent(typeof(CanvasGroup), typeof(Animator))]
    public class UIPanel : MonoBehaviour
    {
        public void SetAlpha(float alpha)
        {
            if (!TryGetComponent(out CanvasGroup component))
                return;
            
            component.alpha = alpha;
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
            if (!TryGetComponent(out Animator animator)) 
                return;
            
            if (animator.runtimeAnimatorController == null)
                return;
                
            animator.Rebind();
            animator.Update(0f);
        }
    }
}