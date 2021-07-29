// EventSystem.CS
// Author: Chris Nyman / Keirron Stach
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace AFewDragons.DragonEvent
{
    /// <summary>
    /// Represent and event system
    /// </summary>
    public class EventSystem : MonoBehaviour
    {
        private Dictionary<string, UnityEvent> eventDictionary;
        private Dictionary<Type, object> eventDictionaryType;

        public EventSystem()
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
            eventDictionaryType = new Dictionary<Type, object>();
        }

        public void StartListening(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                eventDictionary.Add(eventName, thisEvent);
            }
        }

        public void StopListening(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public void TriggerEvent(string eventName)
        {
            UnityEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }

        public void StartListening<T>(string eventName, UnityAction<T> listener)
        {
            Dictionary<string, GenericEvent<T>> typeDictionary = null;
            object baseDict = null;

            if(eventDictionaryType.TryGetValue(typeof(T), out baseDict))
            {
                typeDictionary = baseDict as Dictionary<string, GenericEvent<T>>;
            }
            else
            {
                typeDictionary = new Dictionary<string, GenericEvent<T>>();
                eventDictionaryType.Add(typeof(T), typeDictionary);
            }

            GenericEvent<T> thisEvent = null;

            if (typeDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new GenericEvent<T>();
                thisEvent.AddListener(listener);
                typeDictionary.Add(eventName, thisEvent);
            }
        }

        public void StopListening<T>(string eventName, UnityAction<T> listener)
        {
            Dictionary<string, GenericEvent<T>> typeDictionary = null;
            object baseDict = null;

            if (eventDictionaryType.TryGetValue(typeof(T), out baseDict))
            {
                typeDictionary = baseDict as Dictionary<string, GenericEvent<T>>;
            }
            else
            {
                return;
            }

            GenericEvent<T> thisEvent = null;
            if (typeDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public void TriggerEvent<T>(string eventName, T param)
        {
            Dictionary<string, GenericEvent<T>> typeDictionary = null;
            object baseDict = null;

            if (eventDictionaryType.TryGetValue(typeof(T), out baseDict))
            {
                typeDictionary = baseDict as Dictionary<string, GenericEvent<T>>;
            }
            else
            {
                return;
            }

            GenericEvent<T> thisEvent = null;
            if (typeDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(param);
            }
        }

        private class GenericEvent<T> : UnityEvent<T>
        {

        }
    }
}

