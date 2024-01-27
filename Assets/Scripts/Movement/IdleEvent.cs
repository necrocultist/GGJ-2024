using UnityEngine;
using System;

[DisallowMultipleComponent]
public class IdleEvent : MonoBehaviour
{
    public event Action<IdleEvent> OnIdle;

    public void CallIdleEvent()
    {
        Debug.Log("Idle Event Called");
        OnIdle?.Invoke(this);
    }
}