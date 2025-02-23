using UnityEngine;

namespace PhantomEngine
{
    public class UIUtility
    {
        public static T Bind<T>(GameObject target) where T : Component
        {
            var hasComponent = target.TryGetComponent(out T component);
            if (!hasComponent)
            {
                component = target.AddComponent<T>();
            }

            return component;
        }
    }
}