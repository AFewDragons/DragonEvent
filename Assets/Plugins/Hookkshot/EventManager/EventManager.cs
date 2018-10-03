using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KestCast.EventManager
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, UnityEvent> eventDictionary;

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
            if (eventDictionary == null)
                instance.eventDictionary = new Dictionary<string, UnityEvent>();
        }

        public static void StartListening(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, UnityAction listener)
        {
            if (_instance == null) return;
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(string eventName)
        {
            if (_instance == null) return;
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}

