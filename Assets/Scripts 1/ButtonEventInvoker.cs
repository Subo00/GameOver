using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEventInvoker : MonoBehaviour
{
    // A single UnityEvent object is needed and necessary because
    // each button that has a ButtonEventInvoker attached to it
    // should only invoke one event. The action taken upon invocation
    // is customized in the inspector.
    public UnityEvent buttonEvent;

    public void InvokeButtonEvent()
    {
        buttonEvent.Invoke();
    }
}
