using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent<T> : ScriptableObject
{
    private List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();

    public void Raise(T value)
    {
        // Iterate backwards to prevent bug when listeners remove themselves 
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(value);
        }
    }

    public void RegisterListener(GameEventListener<T> listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener<T> listener)
    {
        if (!listeners.Contains(listener))
            listeners.Remove(listener);
    }
}