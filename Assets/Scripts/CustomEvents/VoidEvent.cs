using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Event/Void Event")]
public class VoidEvent : ScriptableObject
{
    private List<VoidEventListener> listeners = new List<VoidEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(VoidEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(VoidEventListener listener)
    {
        listeners.Remove(listener);
    }
}