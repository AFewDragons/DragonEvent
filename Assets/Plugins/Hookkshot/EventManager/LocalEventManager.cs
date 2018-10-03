using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KestCast.EventManager
{
    /// <summary>
    /// An event manager to handle event locally to one gameobject
    /// </summary>
    public class LocalEventManager : MonoBehaviour
    {
        //Our event dictionary
        private Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

        /// <summary>
        /// Start listening to an event trigger
        /// </summary>
        /// <param name="gameObject">The gameobject calling the event manager.</param>
        /// <param name="eventName">Name of the event to listen too.</param>
        /// <param name="listener">The listener that will be registered. This will be called every time the event fires.</param>
        public static void StartListening(GameObject gameObject, string eventName, UnityAction listener)
        {
            if(gameObject == null)
            {
                Debug.LogWarning("The game object passed in was null.");
                return;
            }
            var localEventManager = gameObject.GetComponent<LocalEventManager>();

            if (localEventManager == null)
            {
                Debug.LogWarning("No local event manager was found on " + gameObject.name + ".");
                return;
            }

            localEventManager.StartListening(eventName, listener);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="behaviour"></param>
        /// <param name="eventName"></param>
        /// <param name="listener"></param>
        public static void StopListening(GameObject gameObject, string eventName, UnityAction listener)
        {
            if (gameObject == null)
            {
                Debug.LogWarning("The game object passed in was null.");
                return;
            }
            var localEventManager = gameObject.GetComponent<LocalEventManager>();

            if (localEventManager == null)
            {
                Debug.LogWarning("No local event manager was found on " + gameObject.name + ".");
                return;
            }

            localEventManager.StopListening(eventName, listener);
        }

        public void StopListening(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(GameObject gameObject, string eventName)
        {
            if (gameObject == null)
            {
                Debug.LogWarning("The game object passed in was null.");
                return;
            }
            var localEventManager = gameObject.GetComponent<LocalEventManager>();

            if (localEventManager == null)
            {
                Debug.LogWarning("No local event manager was found on " + gameObject.name + ".");
                return;
            }

            localEventManager.TriggerEvent(eventName);
        }

        public void TriggerEvent(string eventName)
        {
            UnityEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}

