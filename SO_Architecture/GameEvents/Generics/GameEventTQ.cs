using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace FeTo.SOArchitecture
{
    public abstract class GameEvent<T, Q> : ScriptableObject where Q : UnityEvent<T>
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<GameEventListener<T, Q>> eventListeners = new();

        public System.Type GetEventType() {
            return typeof(T);
        }

        public void UIRaise(T value) {
#if UNITY_EDITOR
            Debug.Log($"FeTo: {this.name} Raised By UI");
#endif
            DoRaise(value);
        }

        public void Raise(T value, [CallerMemberName] string callerName = "") {
#if UNITY_EDITOR
            Debug.Log($"FeTo: {this.name} Raised By {callerName}");
#endif
            DoRaise(value);
        }

        private void DoRaise(T value)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(value);
        }

        public void RegisterListener(GameEventListener<T, Q> listener) {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener<T, Q> listener) {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}