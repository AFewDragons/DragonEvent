# Unity Simple Messaging

Unity Simple Messaging is an event messaging system for unity.

This type of system allows many components of your system to listen to types of events without having to know who the caller is. This is very helpful for decoupling your components and making smaller functioning code that doesnt have a stack of dependicies.

## Methods

#### StartListening(string eventName, UnityAction listener)
This method register a function as a listener for the event system to call when ever an event of the same name is triggered.
The UnityAction can just be a pointer to a parameterless function.

#### StopListening(string eventName, UnityAction listener)
This method will remove a listener from the event system of the given event name.

*Note.. Beofer destorying a script it is good to call stopListening on its methods first to stop memory leaking.*

#### TriggerEvent(string eventName)
All listeners of the given eventName will be invoked and run.

### Generics
These methods are the same as above put accept a parameter of a given type for the function. This allows the event system to pass an object through to the listener.

#### StartListenin<T>(string eventName, UnityAction<T> listener)
T is the type you want to be passed through when the event is triggered. 

**Example**
```
StartListening<int>("PlayerTookDamage", FlashRed);
public void FlashRed(int damage){}
```

This allows us to make a listener for when the player takes damage and maybe we can flash a brighter red depending on the damage received.

#### StopListening<T>(string eventName, UnityASction<T> listener)
T is the type you want to be passed through when the event is triggered. 

#### TriggerEvent<T>(string eventName, T parameter)
Here is where we trigger an event with the parameter
**Example**
```
TriggerEvent<int>("PlayerTookDamage", 7);
```

As yopu can see from our example earlier we can now pass the amount of damage through to listeners.