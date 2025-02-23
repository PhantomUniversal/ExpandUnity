using System;
using UnityEngine;

namespace PhantomEngine
{
    public abstract class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected abstract void OnInitialized();

        protected abstract void OnDisposed();
        
        
        private static T singletonInstance;
        private static object singletonLock = new();
        private static bool singletonQuit;
        
        public static T Instance
        {
            get
            {
                if (singletonQuit)
                    return null;

                lock (singletonLock)
                {
                    singletonInstance = (T)FindFirstObjectByType(typeof(T));
                    if (singletonInstance != null)
                        return singletonInstance;

                    var singletonObject = new GameObject();
                    singletonInstance = singletonObject.AddComponent<T>();
                    singletonInstance.name = typeof(T).Name + "(Singleton)";
                    
                    DontDestroyOnLoad(singletonObject);

                    return singletonInstance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (singletonInstance == null)
            {
                singletonInstance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (singletonInstance != this)
            {
                Destroy(gameObject);
            }
        }

        protected void Start()
        {
            OnInitialized();
        }

        protected virtual void OnDestroy()
        {
            singletonInstance = null;
            OnDisposed();
        }

        public virtual void OnApplicationQuit()
        {
            singletonInstance = null;
            singletonQuit = true;
            OnDisposed();
        }
    }
}