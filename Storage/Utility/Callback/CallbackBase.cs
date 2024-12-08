using UnityEngine;

namespace PhantomEngine
{
    public class CallbackBase : MonoBehaviour, ICallbackBase 
    {
        private void Start()
        {
            CallbackManager.Instance.AddCallback(this);
        }

        private void OnDestroy()
        {
            CallbackManager.Instance.RemoveCallback(this);
        }

        
        public void OnEnter()
        {
            
        }

        public void OnUpdate()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}