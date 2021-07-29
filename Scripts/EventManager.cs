// EventManager.cs
// Author: Keirron Stach
using UnityEngine;
using UnityEngine.Events;

namespace AFewDragons.DragonEvent
{
    /// <summary>
    /// Event manager for global events
    /// </summary>
    public static class EventManager
    {
        private static EventSystem eventSystem = new EventSystem(); 

        public static void StartListening(string eventName, UnityAction listener)
        {
            eventSystem.StartListening(eventName, listener);
        }

        public static void StopListening(string eventName, UnityAction listener)
        {
            eventSystem.StopListening(eventName, listener);
        }

        public static void TriggerEvent(string eventName)
        {
            eventSystem.TriggerEvent(eventName);
        }

        public static void StartListening<T>(string eventName, UnityAction<T> listener)
        {
            eventSystem.StartListening(eventName, listener);
        }

        public static void StopListening<T>(string eventName, UnityAction<T> listener)
        {
            eventSystem.StopListening(eventName, listener);
        }

        public static void TriggerEvent<T>(string eventName, T param)
        {
            eventSystem.TriggerEvent(eventName, param);
        }
    }
}

