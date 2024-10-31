using UnityEngine;

namespace PhantomEngine
{
    public static class Log
    {
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Message(object message)
        {
            Debug.Log(message);
        }
        
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Message(object message, string color)
        {
            Debug.Log($"<color={color}>{message}</color>");
        }
        
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Message(object message, Object context)
        {
            Debug.Log(message, context);
        }
        
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Message(object message, string color, Object context)
        {
            Debug.Log($"<color={color}>{message}</color>", context);
        }
    }
}