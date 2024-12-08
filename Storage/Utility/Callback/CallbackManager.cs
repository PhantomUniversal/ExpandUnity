using System;
using System.Collections.Generic;

namespace PhantomEngine
{
    public class CallbackManager : GenericSingleton<CallbackManager>
    {
        private readonly Dictionary<Type, ICallbackBase> callbackContainer = new();
        
        
        protected override void OnInitialized()
        {
            
        }

        protected override void OnDisposed()
        {
            
        }


        public void AddCallback(ICallbackBase target)
        {
            if (callbackContainer.ContainsKey(target.GetType()))
                return;
            
            callbackContainer.TryAdd(target.GetType(), target);
            target.OnEnter();
        }

        public void RemoveCallback(ICallbackBase target)
        {
            if (!callbackContainer.ContainsKey(target.GetType()))
                return;
            
            callbackContainer.Remove(target.GetType());
            target.OnExit();
        }
        
        public ICallbackBase FindCallback<T>()
        {
            return callbackContainer.GetValueOrDefault(typeof(T));
        }

        public void ClearCallback()
        {
            if (callbackContainer.Count == 0)
                return;

            foreach (var callback in callbackContainer.Values)
            {
                callback?.OnExit();
            }
            
            callbackContainer.Clear();
        }

        
        public void UpdateCallback<T>()
        {
            var target = callbackContainer.GetValueOrDefault(typeof(T));
            target?.OnUpdate();
        }
        
        public void UpdateCallback()
        {
            if (callbackContainer.Count == 0)
                return;

            foreach (var callback in callbackContainer.Values)
            {
                callback?.OnUpdate();
            }
        }
    }
}