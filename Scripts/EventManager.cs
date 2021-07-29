// EventManager.cs
// Author: Keirron Stach
using UnityEngine;
using UnityEngine.Events;

namespace AFewDragons.DragonEvent
{
    /// <summary>
    /// Event manager for global events
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        private EventSystem eventSystem;

        private static EventManager _instance;
        private static EventManager instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<EventManager>();
                    if (_instance == null)
                    {
                        Debug.LogError("There needs to be an instance of EventManager in the scene.");
                    }
                    else
                    {
                        _instance.Init();
                    }
                }
                return _instance;
            }
        }

        private void Init()
        {
            eventSystem = new EventSystem();
        }

        public static void StartListening(string eventName, UnityAction listener)
        {
            instance.eventSystem.StartListening(eventName, listener);
        }

        public static void StopListening(string eventName, UnityAction listener)
        {
            instance.eventSystem.StopListening(eventName, listener);
        }

        public static void TriggerEvent(string eventName)
        {
            instance.eventSystem.TriggerEvent(eventName);
        }

        public static void StartListening<T>(string eventName, UnityAction<T> listener)
        {
            instance.eventSystem.StartListening(eventName, listener);
        }

        public static void StopListening<T>(string eventName, UnityAction<T> listener)
        {
            instance.eventSystem.StopListening(eventName, listener);
        }

        public static void TriggerEvent<T>(string eventName, T param)
        {
            instance.eventSystem.TriggerEvent(eventName, param);
        }
    }
}

