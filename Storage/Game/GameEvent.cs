using System.Collections.Generic;
using UnityEngine;

namespace PhantomEngine
{
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameListener> listeners = new();

        public void Raise()
        {
            foreach (var listener in listeners)
            {
                listener.OnEventRaised();
            }
        }

        public void Register(GameListener listener)
        {
            listeners.Add(listener);
        }

        public void Unregister(GameListener listener)
        {
            listeners.Remove(listener);
        }
    }
}