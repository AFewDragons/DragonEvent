// LocalEventManager.cs
// Author: Keirron Stach
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
        private EventSystem eventSystem;

        public LocalEventManager()
        {
            eventSystem = new EventSystem();
        }

        /// <summary>
        /// Start listening to an event trigger
        /// </summary>
        /// <param name="gameObject">The gameobject calling the event manager.</param>
        /// <param name="eventName">Name of the event to listen too.</param>
        /// <param name="listener">The listener that will be registered. This will be called every time the event fires.</param>
        public static void StartListening(GameObject gameObject, string eventName, UnityAction listener)
        {
            if (gameObject == null)
            {
                Debug.LogWarning("The game object passed in was null.");
                return;
            }
            var localEventManager = gameObject.GetComponent<LocalEventManager>();

            if (localEventManager == null)
            {
                localEventManager = gameObject.AddComponent<LocalEventManager>();
            }

            localEventManager.StartListening(eventName, listener);
        }

        public void StartListening(string eventName, UnityAction listener)
        {
            eventSystem.StartListening(eventName, listener);
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
            eventSystem.StopListening(eventName, listener);
        }

        /// <summary>
        /// Trigger an event
        /// </summary>
        /// <param name="gameObject">The game object on which the event will occur</param>
        /// <param name="eventName">The event identifier</param>
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
            eventSystem.TriggerEvent(eventName);
        }

        /// <summary>
        /// Start listening to an event trigger with a specified type
        /// </summary>
        /// <param name="gameObject">The gameobject calling the event manager.</param>
        /// <param name="eventName">Name of the event to listen too.</param>
        /// <param name="listener">The listener that will be registered. This will be called every time the event fires.</param>
        public static void StartListening<T>(GameObject gameObject, string eventName, UnityAction<T> listener)
        {
            if (gameObject == null)
            {
                Debug.LogWarning("The game object passed in was null.");
                return;
            }
            var localEventManager = gameObject.GetComponent<LocalEventManager>();

            if (localEventManager == null)
            {
                localEventManager = gameObject.AddComponent<LocalEventManager>();
            }

            localEventManager.StartListening(eventName, listener);
        }

        public void StartListening<T>(string eventName, UnityAction<T> listener)
        {
            eventSystem.StartListening(eventName, listener);
        }

        /// <summary>
        /// Stop listening to an event with a specified type.
        /// </summary>
        /// <param name="behaviour"></param>
        /// <param name="eventName"></param>
        /// <param name="listener"></param>
        public static void StopListening<T>(GameObject gameObject, string eventName, UnityAction<T> listener)
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

        public void StopListening<T>(string eventName, UnityAction<T> listener)
        {
            eventSystem.StopListening(eventName, listener);
        }

        /// <summary>
        /// Trigger an event
        /// </summary>
        /// <param name="gameObject">The game object on which the event will occur</param>
        /// <param name="eventName">The event identifier</param>
        public static void TriggerEvent<T>(GameObject gameObject, string eventName, T param)
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

            localEventManager.TriggerEvent(eventName, param);
        }

        public void TriggerEvent<T>(string eventName, T param)
        {
            eventSystem.TriggerEvent(eventName, param);
        }
    }
}

