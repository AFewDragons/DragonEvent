using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace KestCast.EventManager
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, UnityEvent> eventDictionary;
        private Dictionary<Type, object> eventDictionaryType;

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
            if (eventDictionaryType == null)
                instance.eventDictionaryType = new Dictionary<Type, object>();
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

        public static void StartListening<T>(string eventName, UnityAction<T> listener)
        {
            if (_instance == null) return;
            
            Dictionary<string, GenericEvent<T>> thisDictionary = null;
            object baseDict = null;

            if(instance.eventDictionaryType.TryGetValue(typeof(T), out baseDict))
            {
                thisDictionary = baseDict as Dictionary<string, GenericEvent<T>>;
            }
            else
            {
                thisDictionary = new Dictionary<string, GenericEvent<T>>();
                instance.eventDictionaryType.Add(typeof(T),thisDictionary);
            }

            GenericEvent<T> thisEvent = null;

            if (thisDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new GenericEvent<T>();
                thisEvent.AddListener(listener);
                thisDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening<T>(string eventName, UnityAction<T> listener)
        {
            if (_instance == null) return;

            Dictionary<string, GenericEvent<T>> thisDictionary = null;
            object baseDict = null;

            if (instance.eventDictionaryType.TryGetValue(typeof(T), out baseDict))
            {
                thisDictionary = baseDict as Dictionary<string, GenericEvent<T>>;
            }
            else
            {
                return;
            }

            GenericEvent<T> thisEvent = null;
            if (thisDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent<T>(string eventName, T param)
        {
            if (_instance == null) return;

            Dictionary<string, GenericEvent<T>> thisDictionary = null;
            object baseDict = null;

            if (instance.eventDictionaryType.TryGetValue(typeof(T), out baseDict))
            {
                thisDictionary = baseDict as Dictionary<string, GenericEvent<T>>;
            }
            else
            {
                return;
            }

            GenericEvent<T> thisEvent = null;
            if (thisDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(param);
            }
        }

        private class GenericEvent<T> : UnityEvent<T>
        {

        }
    }
}

