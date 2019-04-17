# Unity Simple Messaging

Unity Simple Messaging is an event messaging system for unity.

This type of system allows many components of your system to listen to types of events without having to know who the caller is. This is very helpful for decoupling your components and making smaller functioning code that doesnt have a stack of dependicies.

A good example is to think of a UI system that is able to listen to when a playerHurt event is trigger. The player scripts don't need to know about the UI but the UI can get all the information it needs when the event is fired.

## Release

**Current Version:** 1.2

### Coming Changes
- Debugging event chains

### History
**1.2**
- Added base class for bother event manager to call on, centralizing event logic
- Local Event Manager can no use generic based events

**1.1**
- Added Generics for previously developed methods
- Made it so a LocalEventManager will be created if it is not on an object you are calling it on. (this makes development a bit faster)

**1.0**
- Added StartListening, StopListnening and TriggerEvent

## Methods



#### Start Listening
```
StartListening(string eventName, UnityAction listener)
```
This method register a function as a listener for the event system to call when ever an event of the same name is triggered.
The UnityAction can just be a pointer to a parameterless function.

#### Stop Listening
```
StopListening(string eventName, UnityAction listener)
```
This method will remove a listener from the event system of the given event name.

*Note.. Beofer destorying a script it is good to call stopListening on its methods first to stop memory leaking.*

#### Trigger Event
```
TriggerEvent(string eventName)
```
All listeners of the given eventName will be invoked and run.

### Generics
These methods are the same as above put accept a parameter of a given type for the function. This allows the event system to pass an object through to the listener.

#### Start Listening Generic
```
StartListening<T>(string eventName, UnityAction<T> listener)
```
T is the type you want to be passed through when the event is triggered. 

**Example**
```
StartListening<int>("PlayerTookDamage", FlashRed);
public void FlashRed(int damage){}
```

This allows us to make a listener for when the player takes damage and maybe we can flash a brighter red depending on the damage received.

#### Stop Listening Generic
```
StopListening<T>(string eventName, UnityASction<T> listener)
```
T is the type you want to be passed through when the event is triggered. 

#### Trigger Event Generic
```
TriggerEvent<T>(string eventName, T parameter)
```
Here is where we trigger an event with the parameter
**Example**
```
TriggerEvent<int>("PlayerTookDamage", 7);
```

As you can see from our example earlier we can now pass the amount of damage through to listeners.

## Examples

#### Play sound on event
In this example we register a listener for when ever a door gets opened in the game. A sound is played whenever that even is now called.
```
void onEnabled(){
  EventManager.StartListening("DoorOpened", OnDoorOpen);
}

void onDisable(){
  EventManager.StopListening("DoorOpened", OnDoorOpen);
}

private void OnDoorOpen(){
  PlaySound(doorOpen);
}
```

#### Player hit
The player hit example shows a very good use case allowing use to listen to when a player gets it and flash a ui element red.

**UI Class**
```
void onEnabled(){
  EventManager.StartListening<int>("PlayerHit", FlashRed);
}

void onDisable(){
  EventManager.StopListening<int>("PlayerHit", FlashRed);
}

private void FlashRed(int damage){
	renderer.color = new Color(1,0,0);
}
```

**Enemy Class**
```
private HitPlayer(Player player, int damage) {
	player.TakeDamage(damage);
	EventManager.TriggerEvent<int>("PlayerHit", damage);
}
```