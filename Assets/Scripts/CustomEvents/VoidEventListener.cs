using UnityEngine;
using UnityEngine.Events; 

public class VoidEventListener : MonoBehaviour
{
    public VoidEvent Event;
    public UnityEvent Response;

    public void OnEventRaised()
    {
        Response?.Invoke();
    }

    private void OnEnable()
    {
        if (Event != null)
            Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (Event != null)
            Event.UnregisterListener(this);
    }
}