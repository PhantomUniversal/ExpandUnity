using UnityEngine;
using UnityEngine.Events;

namespace PhantomEngine
{
    public class GameListener : MonoBehaviour
    {
        public GameEvent listenerEvent;
        public UnityEvent listenerResponse;


        private void OnEnable()
        {
            listenerEvent.Register(this);
        }

        private void OnDisable()
        {
            listenerEvent.Unregister(this);
        }

        public void OnEventRaised()
        {
            listenerResponse.Invoke();
        }
    }
}